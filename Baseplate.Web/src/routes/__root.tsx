import { Outlet, createRootRoute } from '@tanstack/react-router'
import { TanStackRouterDevtoolsPanel } from '@tanstack/react-router-devtools'
import { TanstackDevtools } from '@tanstack/react-devtools'

import Header from '../components/layout/Header'
import {ThemeProvider} from "@/components/ui/theme-provider.tsx";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";

import {Toaster} from '@/components/ui/sonner.tsx';
const queryClient = new QueryClient()
export const Route = createRootRoute({
  component: () => (
    <>
        <QueryClientProvider client={queryClient}>
        <ThemeProvider>
            <Toaster />
            <Header />
      <Outlet />
      <TanstackDevtools
        config={{
          position: 'bottom-left',
        }}
        plugins={[
          {
            name: 'Tanstack Router',
            render: <TanStackRouterDevtoolsPanel />,
          },
        ]}
      />
        </ThemeProvider>
        </QueryClientProvider>
    </>
  ),
})
