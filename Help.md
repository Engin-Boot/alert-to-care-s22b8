# Alert To Care Web API [Segment 1]

### 1. Alert-to-Care
The controller and repositories reside here. All the API URL are listed below.
#### ICU Controller
| Segment | URL| Request Type | Body |
| ------ | ------ | ------ | ------ |
| ICU - Get all ICU | http://localhost:5000/api/ICUConfig | GET | - |
| ICU - Get by ICU ID | http://localhost:5000/api/ICUConfig/{icuNumber} | GET | - |
| ICU - Add new ICU | http://localhost:5000/api/ICUConfig/register | POST | {"NumberOfBeds": 5,"Layout": "C"}|
| ICU - Update ICU  | http://localhost:5000/api/ICUConfig/{icuNumber} | PUT | {"NumberOfBeds": 5,"Layout": "C"}
| ICU - Delete ICU  | http://localhost:5000/api/ICUConfig/{icuNumber} | DELETE | - |


#### Occupancy Management Controller
| Segment | URL| Request Type | Body |
| ------ | ------ | ------ | ------ |
| Patient - Find Patient by ICU ID | http://localhost:5000/api/OccupancyManagement/{icuNumber} | GET | - |
| Patient - Find Patient by Patient ID | http://localhost:5000/api/OccupancyManagement/GetPatientById/{PatientNumber} | GET | - |
| Patient - Find Patient by ICU ID |http://localhost:5000/api/OccupancyManagement/{icuNumber}|POST|{"name":"XXXX","age":22,"bloodGroup": "AB+","address": "Jaipur","bedNumber": 3} |
| Patient - Update Patient | http://localhost:5000/api/OccupancyManagement/{PatientNumber} |PUT|{"name":"XXXX","age":22,"bloodGroup": "AB+","address": "Jaipur", "icuId": 27,"bedNumber": 3} |
| Patient - Delete Patient | http://localhost:5000/api/OccupancyManagement/{PatientNumber} | DELETE | - |

#### Alert Controller
| Segment | URL| Request Type | Body |
| ------ | ------ | ------ | ------ |
| Vitals Alert | http://localhost:5000/api/VitalsAlert | POST |  [ {"id":24,"Vitals":[200,10,200]}] |


### 2. Unit Test
All the unit tests for the Web API are available here.
Link :
```sh
https://github.com/Engin-Boot/alert-to-care-s22b8/tree/main/Alert-To-Care-Unit-Tests
```
### 3. Integration Test
All the Integartion tests for the Web API are available here.
Link :
```sh
https://github.com/Engin-Boot/alert-to-care-s22b8/tree/main/Alert_To_Care.Integration.Tests
```
### 3. Data Producer
An Application which reads patient Vital values from a csv file and sends alerts.
Link :
```sh
https://github.com/Engin-Boot/alert-to-care-s22b8/tree/main/DataProducer
```
# Alert To Care GUI with Angular [Segment 2]
Check out here....
Link :
```sh
https://github.com/TwinkalParmar-16/AlertToCare
```
