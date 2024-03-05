import "./App.css";
import { Outlet } from "react-router-dom";

function App() {
    return (
        <>
            <header>
                <h1>Шапка</h1>
            </header>
            <main>
                <Outlet />
            </main>
            <footer>
                <h2>Подвал</h2>
            </footer>
        </>
    );
}

export default App;
