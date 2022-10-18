import type { NextPage } from 'next'
import Head from 'next/head'
import Image from 'next/image'
import TextArea1 from "../components/TextArea1";

const Home: NextPage = ({}) => {




  return (
    <div className="flex min-h-screen flex-col items-center justify-center py-2">
      <Head>
        <title>Workplanner 1.0</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>
     <TextArea1></TextArea1>

    </div>
  )
}

export default Home
