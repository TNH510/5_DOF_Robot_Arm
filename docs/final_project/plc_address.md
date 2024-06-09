# PLC ADDRESS

Control Memory

| Memory | Usage              |
| ------ | ------------------ |
| M516   | Servo On           |
| M524   | Brake              |
| M528   | Move_J (K2)        |
| M514   | Go Home (K1)       |
| M1999  | Set Home (K0)      |
| M515   | Reset Error        |
| M530   | Move Path (K3)     |
| M532   | Move Path 2 (K5)   |
| M550   | JOG1_F             |
| M551   | JOG1_R             |
| M552   | JOG2_F             |
| M553   | JOG2_R             |
| M554   | JOG3_F             |
| M555   | JOG3_R             |
| M556   | JOG4_F             |
| M557   | JOG4_R             |
| M558   | JOG5_F             |
| M559   | JOG5_R             |
| M600   | Adapt Control (K6) |
| M650   | Change vel (K7)    |
| M700   | Program status     |

Data memory

| Memory              | Usage                             |
| ------------------- | --------------------------------- |
| D1008 --> D1009     | Containt Speed for go_pos         |
| **D1010 --> D1019** | **Data for 5 axis go_pos**        |
| **D1010 --> D1089** | **Data for 4 axis move_path**     |
| D2000 --> D2019     | Data for axis 5 move_path         |
| D1100 --> D1179     | Data for 4 axis move_path_2       |
| D1300 --> D1319     | Data for axis 5 move_path_2       |
| **D1400 --> D1401** | **Data for axis 1 adapt_control** |
| D1402 --> D1403     | Data for axis 2 adapt_control     |
| D1404 --> D1405     | Data for axis 3 adapt_control     |
| D1406 --> D1407     | Data for axis 4 adapt_control     |
| D1408 --> D1409     | Data for axis 5 adapt_control     |
| D2100 --> D2109     | Data for velocity control         |

Temp memory

| Memory | Usage |
| ------ | ----- |
| M560   |       |
| M542   |       |
| M500   |       |
| M502   |       |
| D502   |       |
| M602   |       |
| M604   |       |
| M503   |       |
| D503   |       |
|        |       |

System Memory

| Memory | Usage               |
| ------ | ------------------- |
| M3227  |                     |
| M3247  |                     |
| M3208  |                     |
| M3228  |                     |
| M3248  |                     |
| M3268  |                     |
| M3288  |                     |
| M3202  |                     |
| M3203  |                     |
| M3222  |                     |
| M3223  |                     |
| M3242  |                     |
| M3243  |                     |
| M3262  |                     |
| M3263  |                     |
| M3282  |                     |
| M3283  |                     |
| M2001  |                     |
| M2002  |                     |
| M2003  |                     |
| M2004  |                     |
| M2005  |                     |
| M3200  | Stop Command axis 1 |
| M3220  | Stop Command axis 2 |
| M3240  | Stop Command axis 3 |
| M3260  | Stop Command axis 4 |
| M3280  | Stop Command axis 5 |


