import {useRouter} from "@tanstack/react-router";


export function useNavigateToRoot() {
    const router = useRouter();

    return () => router.navigate({to: '/'});
}