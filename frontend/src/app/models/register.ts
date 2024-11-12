export class SignUpFormModel {
  id?:string;
  Username = "";
  FirstName= "";
  LastName= "";
  Email= "";
  Password= "";
  ConfirmPassword= "";
  Role=0;
  PhoneNumber= "";
  DateOfBirth: string  | null = null;
  TotalHours?=0;
  DoctorCode?= "";
  HouseNumber?=0;
  Street?= "";
  City?= "";
  Country?= "";
}
