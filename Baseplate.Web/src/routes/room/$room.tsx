import {createFileRoute, useRouter} from '@tanstack/react-router'

import {GetRoomApiRequest} from "@/lib/api.ts";

export const Route = createFileRoute('/room/$room')({

    // Data loads BEFORE component renders
    loader: async ({params}) => {
        const roomData = await GetRoomApiRequest(params.room);
        return {roomData};
    },
    component: RoomComponent,
    errorComponent: ({ error }) => <div>Error loading room: {error.message}</div>,
    pendingComponent: () => <div>Loading room...</div>,
});

function RoomComponent() {
  const { room } = Route.useParams();
  const {roomData} = Route.useLoaderData();
  const router = useRouter();

    if (!roomData) {
      router.navigate({
          to: '/'
      })
  } else {
        return (
            <div className="flex flex-col justify-items-center text-center">
                <h1>Room Identifier: {roomData.slug}</h1>
                <h2>Created on {roomData.createdAt.toLocaleString()}</h2>
            </div>
            <div>
                <ChatBarComponent/>
            </div>
        )
    }
}


function ChatBarComponent() {
    return (
        <div>
            <div className="rounded-2xl">
                
            </div>
        </div>
    )
}