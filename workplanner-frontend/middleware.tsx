import {NextResponse} from "next/server";


export default function middleware(req : any) {


    let jwt = req.cookies.get("OurJwt");
    let url = req.url


    if (!jwt && url.includes("/dashboard")) {
        return NextResponse.redirect("http://localhost:3000/");
    }
    if (jwt) {

        const jwtExpire = JSON.parse(atob(jwt.value.split('.')[1]));
        const timeNowSec = Math.round(Date.now() / 1000);

        if (jwtExpire.exp < Math.round(Date.now() / 1000) && url.includes("/dashboard")) {
            return NextResponse.redirect("http://localhost:3000/");
        }

    }

}

