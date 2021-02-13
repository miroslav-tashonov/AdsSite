import { CountryModel } from './country';
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

export class CityEditModel {
  id!: string;
  name!: string;
  postcode!: string;
  countryId: string;
}

export class CityCreateModel {
  name!: string;
  postcode!: string;
  countryId: string;
}
