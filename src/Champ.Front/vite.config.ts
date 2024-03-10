import {defineConfig, loadEnv} from 'vite'
import react from '@vitejs/plugin-react'
import * as path from "node:path";

// https://vitejs.dev/config/
// @ts-ignore
export default ({mode}) => {
    const dotenv = loadEnv(mode, process.cwd());
    console.log(dotenv);
    return defineConfig({
        plugins: [react()],
        resolve: {
            alias: {
                "@": path.resolve(__dirname, "./src"),
                "@shadcn": path.resolve(__dirname, "./@shadcn"),
            }
        },
        server: {
            port: 3000,
            proxy: {
                "^/api": {
                    target: dotenv["VITE_BACKEND_ADDRESS"]
                }
            }
        }
    })

}