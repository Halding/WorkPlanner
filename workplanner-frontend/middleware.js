import {NextResponse} from "next/server";


export default function middleware(req) {


    let jwt = req.cookies.get("OurJwt");
    let url = req.url


    if (!jwt && url.includes("/dashboard")) {
        return NextResponse.redirect("http://localhost:3000/login");
    }
    if (jwt) {
        const jwtExpire = JSON.parse(atob(jwt.split('.')[1]));
        const timeNowSec = Math.round(Date.now() / 1000);

        console.log("expire date = " + jwtExpire.exp);
        console.log("datetime now = " + Math.round(Date.now() / 1000));

        if (jwtExpire.exp < Math.round(Date.now() / 1000) && url.includes("/dashboard")) {
            return NextResponse.redirect("http://localhost:3000/login");
        }

    }

}


// const jwtExpire = JSON.parse(atob(jwt.split('.')[1]));
// const timeNowSec = Math.round(Date.now() / 1000);
//
// console.log("expire date = " + jwtExpire.exp);
// console.log("datetime now = " + Math.round(Date.now() / 1000));


// if(jwtExpire.exp < Math.round(Date.now() / 1000) && url.includes("/dashboard")) {
//
//     return NextResponse.redirect("http://localhost:3000/login");
// }