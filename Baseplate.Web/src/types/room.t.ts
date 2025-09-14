

export interface Room {
    
}

export interface Message {
    messageContent: string;
    createdAt: Date;
    attachments: Attachment[];
}

export interface Attachment {
    attachmentExtension: string;
    attachmentName: string;
    attachmentSizeBytes: number;
    attachmentUrl: string;
    createdAt: Date;
}
