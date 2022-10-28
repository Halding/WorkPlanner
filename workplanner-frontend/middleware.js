import { NextResponse} from "next/server";

import { NextRequest } from 'next/server'




export default function middleware(req) {

    let verify = req.cookies.get("OurJwt");
    let url = req.url

    if(!verify && url.includes("/dashboard")) {
        return NextResponse.redirect("http://localhost:3000/login");
    }
}



