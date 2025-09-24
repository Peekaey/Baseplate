import type {CreateMessageResult, CreateRoomResult} from "@/types/result.t.ts";
import type {CreateRoomResponse, GetRoomResponse} from "@/types/response.t.ts";


const base_url:string  = import.meta.env.VITE_BACKEND_URL as string;

export async function CreateRoomApiRequest(): Promise<CreateRoomResult> {
    const endpoint_url = `${base_url}/api/v1/room/create`
    const response = await fetch(endpoint_url, {
        method: "POST",
        headers: {'Content-Type': 'application/json'},
    })

    if (!response.ok) {
        return {success: false}
    }
    const response_data: CreateRoomResponse = await response.json();
    return {success: true, slug: response_data.slug}
}

export async function CreateMessageApiRequest(messageContent: string, roomId: string): Promise<CreateMessageResult> {
    const endpoint_url = `${base_url}/api/v1/message/create`
    const response = await fetch(endpoint_url, {
        method: "POST",
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({
            message: messageContent,
            roomId: roomId,
        })
    })
    
    if (!response.ok) {
        return {success: false}
    }
    const response_data: CreateMessageResult = await response.json();
    return {success: true, message: response_data.message}
}

export async function GetRoomApiRequest(slug: string): Promise<GetRoomResponse | undefined> {
    const endpoint_url = `${base_url}/api/v1/room/get/${slug}`
    
    try {
    const response = await fetch(endpoint_url, {
        method: "GET",
        headers: {'Content-Type': 'application/json'},
    })
    if (!response.ok) {
        if (response.status === 404) {
            throw new Error(`Room "${slug}" does not exist`);
        }
        if (response.status === 403) {
            throw new Error(`Access denied to room "${slug}"`);
        }
        if (response.status >= 500) {
            throw new Error('Server error. Please try again later.');
        }
        throw new Error(`Failed to join room: ${response.status}`);
    }
    const roomData :GetRoomResponse = await response.json();
    return roomData
    } catch (error) {
        throw new Error('Unexpected error when attempting to join room')
    }
}