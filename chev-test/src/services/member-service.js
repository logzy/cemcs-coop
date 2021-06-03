import axios from "axios";
// import {invoice} from '@/InterfaceType/IVehicle';
export default class VehicleService {
  constructor() {
    // this.API_URL = process.env.VUE_APP_API_URL;
    this.token = localStorage.getItem('token');
  }
  async upload(formData) {
    const url = `${process.env.VUE_APP_API_URL}/photos/upload`;
    return await axios.post(url, formData,{
      headers: { Authorization: `Bearer ${this.token}` }
    })
        // get data
        .then(x => x.data)
        // add url field
        .then(x => x.map(img => Object.assign({},
            img, { url: `${process.env.VUE_APP_API_URL}/images/${img.id}` })));
  }  
}
