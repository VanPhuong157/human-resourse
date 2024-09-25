// src/App.test.js
import React from 'react'
import {render, screen} from '@testing-library/react'
import {BrowserRouter} from 'react-router-dom'
import App from './App'
import {QueryClient, QueryClientProvider} from 'react-query'

const queryClient = new QueryClient()

test('renders header component', () => {
  render(
    <React.StrictMode>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <App />
        </BrowserRouter>
        ,
      </QueryClientProvider>
    </React.StrictMode>,
  )
  const headerElement = screen.getByText(/SHRS/i)
  expect(headerElement).toBeInTheDocument()
})
