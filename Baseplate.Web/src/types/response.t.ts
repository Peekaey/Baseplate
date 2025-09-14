import type {Message} from "@/types/room.t.ts";

export interface CreateRoomResponse {
    slug: string;
}

export interface GetRoomResponse {
    slug: string;
    createdAt: Date;
    messages: Message[];
}
