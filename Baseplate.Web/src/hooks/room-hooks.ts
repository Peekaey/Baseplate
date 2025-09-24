import {useRouter} from "@tanstack/react-router";
import {useMutation, useQueryClient} from "@tanstack/react-query";
import {CreateRoomApiRequest, ValidateRoomApiRequest} from "@/lib/api.ts";
import {toast} from "sonner";
import type {CreateRoomResult, GetRoomResult, ValidateRoomResult} from "@/types/result.t.ts";


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

export function useJoinRoom() {
    const router = useRouter();
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (slug: string) => ValidateRoomApiRequest(slug),
        onSuccess: (validateRoomResult: ValidateRoomResult) => {
            if (validateRoomResult.success == true) {
                if (validateRoomResult.exists) {
                    router.navigate({
                        to: `/room/${validateRoomResult.slug}`
                    })
                } else {
                    toast.error(`Room "${validateRoomResult.slug}" does not exist`)
                }
            } else {
                toast.error("Error when validating room")
            }
        },
        onError: (error) => {
            toast.error("Unexpected error when validating room")
        }
    })
}
