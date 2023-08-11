import {Photo} from "./photo";

export interface Member {
  id: number
  userName: string
  photoUrl: string
  age: number
  knowAs: string
  created: string
  lastActive: string
  introduction: string
  lookingFor: string
  interests: string
  city: string
  country: string
  photos: Photo[]
}
