import {useRouter} from "@tanstack/react-router";
import {useMutation, useQueryClient} from "@tanstack/react-query";
import {CreateRoomApiRequest} from "@/lib/api.ts";
import {toast} from "sonner";
import type {CreateRoomResult, GetRoomResult} from "@/types/result.t.ts";


export function useCreateRoom() {
    const router = useRouter();
    const queryClient = useQueryClient();
    
    return useMutation({
        mutationFn: CreateRoomApiRequest,
        onSuccess: (createRoomResult: CreateRoomResult) => {
            if (createRoomResult.success == false) {
                toast.error("Error when creating room")
            } else {
                router.navigate({
                    to: `/room/${createRoomResult.slug}`})
            }
        },
        onError: (error) => {
            toast.error("Unexpected error when creating room")
        }
    })
}

