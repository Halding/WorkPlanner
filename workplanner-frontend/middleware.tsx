import {NextRequest, NextResponse} from "next/server";


export default function middleware(req : NextRequest) {


    let jwt = req.cookies.get("OurJwt");
    let url = req.url


    if (!jwt && url.includes("/dashboard")) {
        return NextResponse.redirect(`${process.env.NEXT_PUBLIC_CLEAN_URL}`);
    }
    if (jwt) {

        const jwtExpire = JSON.parse(atob(jwt.split('.')[1]));
        const timeNowSec = Math.round(Date.now() / 1000);

        if (jwtExpire.exp < Math.round(Date.now() / 1000) && url.includes("/dashboard")) {
            return NextResponse.redirect(`${process.env.NEXT_PUBLIC_CLEAN_URL}`);
        }

    }

}

