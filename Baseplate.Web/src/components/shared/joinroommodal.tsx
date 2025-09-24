import { useState, useEffect } from 'react'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogCancel,
    AlertDialogAction
} from '@/components/ui/alert-dialog'
import {useJoinRoom} from "@/hooks/room-hooks.ts";

export function JoinRoomModal({ isOpen, onClose }: { isOpen: boolean; onClose: () => void }) {
    const [roomCode, setRoomCode] = useState('')
    const [isLoading, setIsLoading] = useState(false)
    const { mutate: joinRoom } = useJoinRoom();

    const handleClose = () => {
        setRoomCode('')
        setIsLoading(false)
        onClose()
    }
    
    useEffect(() => {
        if (!isOpen) {
            setRoomCode('')
            setIsLoading(false)
        }
    }, [isOpen])

    const handleJoinRoom = async () => {
        if (!roomCode.trim()) {
            return
        }
        setIsLoading(true)
        try {
            joinRoom(roomCode);
            handleClose()
        } catch (err) {
        } finally {
            setIsLoading(false)
        }
    }

    const handleKeyDown = (e: React.KeyboardEvent) => {
        if (e.key === 'Enter' || e.key === 'NumpadEnter' && !isLoading && roomCode.trim()) {
            handleJoinRoom()
        }
    }
    
    const handleOpenChange = (open: boolean) => {
        if (!open) {
            handleClose()
        }
    }

    return (
        <AlertDialog open={isOpen} onOpenChange={handleOpenChange}>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Join Room</AlertDialogTitle>
                    <AlertDialogDescription>
                        Enter the room code to join an existing room.
                    </AlertDialogDescription>
                </AlertDialogHeader>

                <div className="py-4 space-y-4">
                    <div className="space-y-2">
                        <Label htmlFor="room-code" className="text-sm font-medium">
                            Room Code
                        </Label>
                        <Input
                            id="room-code"
                            type="text"
                            placeholder="e.g. cqkvUV8f"
                            value={roomCode}
                            onChange={(e) => setRoomCode(e.target.value)}
                            onKeyDown={handleKeyDown}
                            className="mt-2"
                            autoFocus
                            disabled={isLoading}
                        />
                    </div>
                </div>

                <AlertDialogFooter>
                    <AlertDialogCancel onClick={handleClose} disabled={isLoading}>
                        Cancel
                    </AlertDialogCancel>
                    <AlertDialogAction
                        onClick={handleJoinRoom}
                        disabled={!roomCode.trim() || isLoading}
                    >
                        {isLoading ? 'Joining...' : 'Join Room'}
                    </AlertDialogAction>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}