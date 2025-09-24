import { createFileRoute } from '@tanstack/react-router'
import { Button } from '@/components/ui/button'
import {useCreateRoom} from "@/hooks/room-hooks.ts";
import {useState} from "react";
import {JoinRoomModal} from "@/components/shared/joinroommodal.tsx";
import {useModal} from "@/hooks/modal-hooks.ts";

export const Route = createFileRoute('/')({
  component: App,
})

function App() {
    const { mutate: createRoom } = useCreateRoom();
    const joinModal = useModal()


    return (
    <div className="flex flex-col justify-start items-center min-h-screen pt-70"> 
      <div className="flex flex-col">
      <Button onClick={createRoom}>Create Room</Button>
      <Button onClick={joinModal.openModal} className="mt-2">Join Room</Button>
    </div>

        <JoinRoomModal
            isOpen={joinModal.isOpen}
            onClose={joinModal.closeModal}
        />
    </div>
  )
}
