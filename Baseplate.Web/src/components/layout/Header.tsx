import {ThemeToggle} from "@/components/ui/theme-toggle.tsx";
import {Button} from "@/components/ui/button.tsx";

export default function Header() {
    return (
        <header className=" p-2 flex items-center justify-between">
            <nav className="">
                <div className="flex items-center">
                    <Button className="font-bold text-lg border-none bg-transparent shadow-none hover:bg-transparent text-foreground hover:text-foreground ">Baseplate</Button>
                </div>
            </nav>
            <div className="flex items-center">
                <ThemeToggle />
            </div>
        </header>
    );
}
