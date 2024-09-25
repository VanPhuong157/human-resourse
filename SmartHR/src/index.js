import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from './App'
import {ProSidebarProvider} from 'react-pro-sidebar'
import {BrowserRouter} from 'react-router-dom'
import {UserProvider} from 'context/UserContext'
import {QueryClient, QueryClientProvider} from 'react-query'
const root = ReactDOM.createRoot(document.getElementById('root'))
const queryClient = new QueryClient()

root.render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <UserProvider>
          <ProSidebarProvider>
            <App />
          </ProSidebarProvider>
        </UserProvider>
      </BrowserRouter>
    </QueryClientProvider>
  </React.StrictMode>,
)
