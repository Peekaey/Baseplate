import {useCreateMessage} from "@/hooks/message-hooks.ts";
import {useState} from "react";
import {Textarea} from "@/components/ui/textarea.tsx";

export function ChatBarComponent({ roomId, connectionStatus }: { roomId: string; connectionStatus: string }) {
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
