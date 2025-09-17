import {createFileRoute, useRouter} from '@tanstack/react-router'
import {GetRoomApiRequest} from "@/lib/api.ts";
import {Textarea} from "@/components/ui/textarea.tsx";
import {useCreateMessage} from "@/hooks/message-hooks.ts";
import {useSignalR} from "@/hooks/signalR-hooks.ts"; 
import {useState, useEffect} from "react";
import type {Message} from "@/types/message.t.ts";

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
    const {room} = Route.useParams();
    const {roomData} = Route.useLoaderData();
    const router = useRouter();
    
    if (!roomData) {
        router.navigate({
            to: '/'
        })
        return null;
    }

    // Establish SignalR connection on page load
    const signalRUrl = `http://localhost:5175/messageHub`;
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
                    <h2>Created on {roomData.createdAt.toLocaleString()}</h2>
                    <div className="mt-2">
                        <span className={`px-2 py-1 rounded text-sm ${
                            connectionStatus === 'Connected' ? 'bg-green-100 text-green-800' :
                                connectionStatus === 'Reconnecting' ? 'bg-yellow-100 text-yellow-800' :
                                    'bg-red-100 text-red-800'
                        }`}>
                            {connectionStatus}
                        </span>
                    </div>
                </div>

                {/* Messages*/}
                <div className="flex-1 overflow-y-auto p-4 pt-0 pb-0 space-y-2 w-3/4 mx-auto">
                    {messages.map((msg, index) => (
                        <MessageComponent index={index} message={msg} />
                    ))}
                    <div className="pb-25">
                        
                    </div>
                </div>

                <div className="h-1/5 flex items-center ">
                    <ChatBarComponent
                        roomId={roomData.slug}
                        connection={connection}
                        connectionStatus={connectionStatus}
                    />
                </div>
            </div>
        )
}

function ChatBarComponent({ roomId, connection, connectionStatus }: { roomId: string; connection: any; connectionStatus: string }) {
    const { mutate: createMessage } = useCreateMessage();
    const [message, setMessage] = useState('');

    const handleSubmit = () => {
        if (message.trim().length === 0) return;
        createMessage({ message, roomId });
        setMessage('');
    };

    return (
        <div className="fixed bottom-0 left-0 right-0 p-4 shadow-lg z-20 bg-background">
            <div className="max-w-4xl mx-auto">
                <Textarea
                    wrap="off"
                    className="w-full resize-none overflow-auto leading-relaxed"
                    style={{ whiteSpace: 'pre' }} 
                    placeholder={
                        connectionStatus === 'Connected'
                            ? 'Type your message...'
                            : 'Connecting...'
                    }
                    rows={1}
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    onKeyDown={(e: React.KeyboardEvent<HTMLTextAreaElement>) => {
                        const isEnter = e.key === 'Enter' || e.key === 'NumpadEnter';
                        if (!e.shiftKey && isEnter && !e.nativeEvent.isComposing) {
                            e.preventDefault();
                            handleSubmit();
                        }
                    }}
                    disabled={connectionStatus === 'Failed'}
                />
            </div>
        </div>
    );
}

function MessageComponent({ message, index }: { message: Message; index: number }) {
    return (
        <div className="flex">
            <div
                data-index={index}
                className="
          w-fit max-w-full
          bg-secondary rounded-lg
          px-3 py-2
          whitespace-pre
          overflow-x-auto
          leading-relaxed
          shadow-sm
        "
            >
                {message.messageContent}
            </div>
        </div>
    );
}
