import type {Message} from "@/types/message.t.ts";
import {ConvertDateToLocalString} from "@/lib/utils.ts";


export function MessageComponent({ message, index }: { message: Message; index: number }) {
    const createdAt = ConvertDateToLocalString(message.createdAt);
    
    return (
        <div className="w-full flex">
            <div className="inline-flex flex-col items-start max-w-full">
        <span className="mb-1 text-[10px] text-muted-foreground whitespace-nowrap">
          {createdAt}
        </span>
                <div
                    data-index={index}
                    className="relative inline-block max-w-full bg-secondary rounded-lg px-3 py-2 whitespace-pre-wrap break-words overflow-hidden leading-relaxed shadow-sm"
                >
                    {message.messageContent}
                </div>
            </div>
        </div>
    );
}