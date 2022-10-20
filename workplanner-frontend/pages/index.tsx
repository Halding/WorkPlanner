import type { NextPage } from 'next'
import Head from 'next/head'
import Image from 'next/image'

import TestAll from "../components/TestAll";

const Home: NextPage = ({}) => {




  return (
    <div className="flex min-h-screen flex-col items-center justify-center py-2">
      <Head>
        <title>Workplanner 1.0</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>


        <TestAll></TestAll>

    </div>
  )
}

export default Home
