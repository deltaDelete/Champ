import {defineConfig, loadEnv} from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
// @ts-ignore
export default ({mode}) => {
    const dotenv = loadEnv(mode, process.cwd());
    return defineConfig({
        plugins: [react()],
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