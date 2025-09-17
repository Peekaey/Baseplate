import type {Attachment} from "@/types/room.t.ts";


export interface Message {
    messageContent: string;
    createdAt: Date;
    attachments: Attachment[];
}

