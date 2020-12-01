import { Photo } from './Photo';

 export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    age: number;
    knowAs: string;
    created: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    lookingFor: string;
    intrest: string;
    city: string;
    country: string;
    photos: Photo[];
  }
  
