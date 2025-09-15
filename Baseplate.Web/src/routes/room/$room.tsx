import {createFileRoute, useRouter} from '@tanstack/react-router'

import {GetRoomApiRequest} from "@/lib/api.ts";
import {Textarea} from "@/components/ui/textarea.tsx";
import {useCreateMessage} from "@/hooks/message-hooks.ts";
import {useState} from "react";

export const Route = createFileRoute('/room/$room')({

    // Data loads BEFORE component renders
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
    } else {
        return (
            <div className="min-h-screen flex flex-col">
                <div className=" flex flex-col justify-center items-center text-center pb-20">
                    <h1>Room Identifier: {roomData.slug}</h1>
                    <h2>Created on {roomData.createdAt.toLocaleString()}</h2>
                </div>
                <div className="h-1/5 flex items-center">
                <ChatBarComponent roomId={roomData.slug} />
                </div>
            </div>
        )
    }
}


function ChatBarComponent({roomId}: {roomId: string}) {
    const { mutate: createMessage } = useCreateMessage();
    const [message, setMessage] = useState('');
    const handleSubmit = () => {
        if (message.trim()) {
            createMessage({ message, roomId: roomId });
            setMessage('');
        }
    }
    
    return (
        <div className="fixed bottom-0 left-0 right-0 p-4 shadow-lg z-10">
            <div className="max-w-4xl mx-auto">
                <Textarea
                    className="w-full resize-none"
                    placeholder="Type your message..."
                    rows={1}
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    onKeyUp={(e) => {
                        if (e.key === 'Enter' && !e.shiftKey) {
                            e.preventDefault();
                            handleSubmit();
                        }
                    }}
                />
            </div>
        </div>
    )
}

function MessageComponent(messageContent: string, createdAt: Date) {
    return (
        <div>
            
        </div>
    )
}