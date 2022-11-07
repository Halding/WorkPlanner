import '../styles/globals.css'
import type {AppProps} from 'next/app'
import {
    QueryClient,
    QueryClientProvider,
} from '@tanstack/react-query'
import {SessionProvider} from "next-auth/react";
import { RecoilRoot } from 'recoil';

import {Session} from "next-auth";

const queryClient = new QueryClient()

function MyApp({
                   Component,
                   pageProps,
               }: AppProps<{
    session: Session;
}>) {
    return (
        // <SessionProvider session={pageProps.session}>
            <QueryClientProvider client={queryClient}>
                <RecoilRoot>
                    <Component {...pageProps} />
                </RecoilRoot>
            </QueryClientProvider>
        // </SessionProvider>
    )


}

export default MyApp
