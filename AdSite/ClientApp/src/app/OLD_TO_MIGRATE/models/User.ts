export class User {
  twoFactorEnabled?: boolean;
  phoneNumberConfirmed?: boolean;
  phoneNumber?: string;
  concurrencyStamp?: string;
  securityStamp?: string;
  passwordHash?: string;
  emailConfirmed?: boolean;
  normalizedEmail?: string;
  email?: string;
  normalizedUserName?: string;
  userName?: string;
  id?: string;
  lockoutEnabled?: boolean;
  accessFailedCount?: number;
  role?: string;
  token?: string;
}

export class RegisterUser {
  phone?: string;
  password?: string;
  confirmPassword?: string;
  email?: string;
  countryId?: string;
}

export class ResetPasswordModel {
  oldPassword?: string;
  password?: string;
  confirmPassword?: string;
  email?: string;
  countryId?: string;
}

export class ManageUser {
  phone?: string;
  email?: string;
  countryId?: string;
}
