import { createFileRoute } from '@tanstack/react-router'
import { Button } from '@/components/ui/button'
import {useCreateRoom} from "@/hooks/room-hooks.ts";

export const Route = createFileRoute('/')({
  component: App,
})

function App() {
    const { mutate: createRoom } = useCreateRoom();
  return (
    <div className="flex flex-col justify-start items-center min-h-screen pt-70"> 
      <div className="flex flex-col">
      <Button onClick={createRoom}>Create Room</Button>
      <Button className="mt-2">Join Room</Button>
    </div>
    </div>
  )
}
