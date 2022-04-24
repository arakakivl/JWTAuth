import { Role } from "./role";

export interface UserModel {
    username : string,
    role : Role,
    createdAt : string
}

export interface DecodedToken {
    exp : number
}