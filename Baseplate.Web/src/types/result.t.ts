import type {GetRoomResponse} from "@/types/response.t.ts";

export interface CreateRoomResult {
    success: boolean;
    slug?: string;
}

export interface GetRoomResult {
    success: boolean;
    roomResponse?: GetRoomResponse;
}