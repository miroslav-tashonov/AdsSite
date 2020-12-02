import { CountryModel } from './CountryModel';
import { User } from './User';

export class CityModel {
  id!: string;
  name!: string;
  postcode!: string;
  country!: CountryModel;
  createdAt!: Date;
  modifiedAt!: Date;
  modifiedBy!: User;
  createdBy!: User;
}
