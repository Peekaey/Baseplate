import {createFileRoute, useRouter} from '@tanstack/react-router'
import {GetRoomApiRequest} from "@/lib/api.ts";
import {useSignalR} from "@/hooks/signalR-hooks.ts"; 
import {useEffect} from "react";
import type {Message} from "@/types/message.t.ts";
import {ChatBarComponent} from "@/components/shared/chatbar.tsx";
import {MessageComponent} from "@/components/shared/message.tsx";
import {ConvertDateToLocalString} from "@/lib/utils.ts";

export const Route = createFileRoute('/room/$room')({
    loader: async ({params}) => {
        const roomData = await GetRoomApiRequest(params.room);
        return {roomData};
    },
    component: RoomComponent,
    errorComponent: ({error}) => <div>Error loading room: {error.message}</div>,
    pendingComponent: () => <div>Loading room...</div>,
});

function RoomComponent() {
    const {roomData} = Route.useLoaderData();
    const router = useRouter();
    const convertedDate = ConvertDateToLocalString(roomData.createdAt);
    if (!roomData) {
        router.navigate({
            to: '/'
        })
        return null;
    }

    // Establish SignalR connection on page load
    const signalRUrl =   `${import.meta.env.VITE_BACKEND_URL}/${import.meta.env.VITE_SIGNALR_HUB_URL}`;
    const { connection, messages, connectionStatus, setMessages } = useSignalR(signalRUrl, roomData.messages);

    useEffect(() => {
        if (connection && connectionStatus === 'Connected') {
            // Join the specific room
            connection.invoke("JoinRoom", roomData.slug)
                .catch(err => console.error('Error joining room: ', err));

            // Listen for new messages in this room
            connection.on("ReceiveMessage", (message:Message) => {
                setMessages(prevMessages => [...prevMessages, message]);
            });

            // Cleanup listener when component unmounts
            return () => {
                connection.off("ReceiveMessage");
                connection.invoke("LeaveRoom", roomData.slug)
                    .catch(err => console.error('Error leaving room: ', err));
            };
        }
    }, [connection, connectionStatus, roomData.slug, setMessages]);

    return (
        <div className="min-h-screen flex flex-col">
            <div className="flex flex-col justify-center items-center text-center pb-20">
                <h1>Room Identifier: {roomData.slug}</h1>
                <h2>Created on {convertedDate}</h2>
                <div className="mt-2">
          <span
              className={`px-2 py-1 rounded text-sm ${
                  connectionStatus === 'Connected'
                      ? 'bg-green-100 text-green-800'
                      : connectionStatus === 'Reconnecting'
                          ? 'bg-yellow-100 text-yellow-800'
                          : 'bg-red-100 text-red-800'
              }`}
          >
            {connectionStatus}
          </span>
                </div>
            </div>

            <div className="flex-1 overflow-y-auto">
                <div className="mx-auto w-full max-w-4xl px-4 space-y-2">
                    {messages.map((msg, index) => (
                        <MessageComponent key={index} index={index} message={msg} />
                    ))}
                </div>
            </div>

            {/*Empty Div with enough padding so that the bottom of most recent chatbar message can be rendered*/}
            <div className="pb-25">

            </div>

            <ChatBarComponent roomId={roomData.slug} connectionStatus={connectionStatus}/>
        </div>
    )
}




