export enum StatusEnum {
  StatusGood = 0,
  StatusUninitialized = 1,      // we have not finished intializing
  StatusNoData = 2,             // there are no planned experiments
  StatusNoFilteredData = 3,     // there are experiments, but none match the filter
  StatusOffine = 4,             // iC Data Center is unavailable
  StatusUnauthorized = 5        // not authorized
}
