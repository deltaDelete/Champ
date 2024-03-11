import "./App.css";
import { Outlet } from "react-router-dom";
import { Toaster } from "@shadcn/components/ui/toaster.tsx";

function App() {
    return (
        <>
            <header>
                <h1>Шапка</h1>
            </header>
            <main>
                <Outlet />
            </main>
            <Toaster />
            <footer>
                <h2>Подвал</h2>
            </footer>
        </>
    );
}

export default App;
