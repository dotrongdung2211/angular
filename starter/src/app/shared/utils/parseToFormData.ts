export function parseToFormData(payload: any) {
  const formData: FormData = new FormData();
  Object.keys(payload).forEach((key) => {
    const capitalizeKey = capitalizeFirstLetter(key);
    if (payload[key]) {
      if (payload[key] instanceof Date) {
        const datestr = new Date(payload[key]).toUTCString();
        formData.append(capitalizeKey, datestr);
      }
      else {
        formData.append(capitalizeKey, payload[key]);
      }
    }
  });
  return formData;
}

const capitalizeFirstLetter = (key: string) => {
  return key[0].toUpperCase() + key.substr(1);
}
