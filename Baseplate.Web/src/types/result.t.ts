import type {GetRoomResponse} from "@/types/response.t.ts";
import type {Message} from "@/types/message.t.ts";

export interface CreateRoomResult {
    success: boolean;
    slug?: string;
}

export interface GetRoomResult {
    success: boolean;
    roomResponse?: GetRoomResponse;
}

export interface CreateMessageResult {
    success: boolean;
    message?: Message;
}

export interface ValidateRoomResult {
    success: boolean;
    exists?: boolean;
    slug?: string;
}