import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import PatientRegister from "./pages/PatientRegister.tsx";
import { loader as QRCodePageLoader, QRCodePage } from "@/pages/QRCodePage.tsx";

const router = createBrowserRouter([{
    path: "/",
    element: <App />,
    children: [
        {
            path: "/",
            element: <PatientRegister />,
        },
        {
            path: "/patient/:id/qrcode",
            element: <QRCodePage />,
            loader: QRCodePageLoader
        }
    ]
}]);

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
);
