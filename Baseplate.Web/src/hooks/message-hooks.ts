import {useMutation} from "@tanstack/react-query";
import {CreateMessageApiRequest} from "@/lib/api.ts";
import type {CreateMessageResult} from "@/types/result.t.ts";
import {toast} from "sonner";


export function useCreateMessage() {
    
    return useMutation({
        mutationFn: ({ message, roomId }: { message: string, roomId: string }) =>
            CreateMessageApiRequest(message, roomId),
        onSuccess: (createMessageResult: CreateMessageResult) => {
            if (createMessageResult.success == false) {
                toast.error("Error when creating new message")
            } else {
                
            }
        },
        onError: (error) => {
            toast.error("Error when creating new message")
        }
    })
}