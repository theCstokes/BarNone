import { BaseDataOverride } from "UEye/Data/BaseDataOverride";
import BodyData from "App/Data/Models/BodyData/BodyData";
import BodyDataFrame from "App/Data/Models/BodyDataFrame/BodyDataFrame";
import Joint from "App/Data/Models/Joint/Joint";


export default class BodyExample extends BaseDataOverride<BodyData> {
    public data: BodyData[] = [
        {
            id: 0,
            recordTimeStamp: "2018-01-25T21:11:43.5828646-05:00",
            details:
                {
                    orderedFrames:[
                        {
                            id: 0,
                            timeOfFrame: "00:00:00",
                            timeUntilNextFrame: "00:00:00",
                            bodyDataID: 0,
                            details:{
                                joints: [
                                    {
                                      id: 0,
                                      jointTypeID: 0,
                                      x: 0.25764367,
                                      y: -0.0243732128,
                                      z: 2.250845,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 1,
                                          value: 0,
                                          name: "SpineBase"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 1,
                                      x: 0.25504753,
                                      y: 0.322076,
                                      z: 2.329126,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 2,
                                          value: 1,
                                          name: "SpineMid"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 2,
                                      x: 0.251306653,
                                      y: 0.6538534,
                                      z: 2.392577,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 3,
                                          value: 2,
                                          name: "Neck"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 3,
                                      x: 0.252288878,
                                      y: 0.8202343,
                                      z: 2.469612,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 4,
                                          value: 3,
                                          name: "Head"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 4,
                                      x: 0.0752344057,
                                      y: 0.540959537,
                                      z: 2.28052878,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 5,
                                          value: 4,
                                          name: "ShoulderLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 5,
                                      x: -0.08253022,
                                      y: 0.394778758,
                                      z: 2.154374,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 6,
                                          value: 5,
                                          name: "ElbowLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 6,
                                      x: -0.0909496248,
                                      y: 0.5788163,
                                      z: 2.16550064,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 7,
                                          value: 6,
                                          name: "WristLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 7,
                                      x: -0.0738523453,
                                      y: 0.5996471,
                                      z: 2.21408725,
                                      jointTrackingStateTypeID: 1,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 8,
                                          value: 7,
                                          name: "HandLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 2,
                                          value: 1,
                                          name: "Inferred"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 8,
                                      x: 0.441709,
                                      y: 0.531547248,
                                      z: 2.301809,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 9,
                                          value: 8,
                                          name: "ShoulderRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 9,
                                      x: 0.5872012,
                                      y: 0.400917619,
                                      z: 2.21036339,
                                      jointTrackingStateTypeID: 1,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 10,
                                          value: 9,
                                          name: "ElbowRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 2,
                                          value: 1,
                                          name: "Inferred"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 10,
                                      x: 0.776389,
                                      y: 0.532582164,
                                      z: 2.29307842,
                                      jointTrackingStateTypeID: 1,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 11,
                                          value: 10,
                                          name: "WristRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 2,
                                          value: 1,
                                          name: "Inferred"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 11,
                                      x: 0.852839231,
                                      y: 0.6123204,
                                      z: 2.34721613,
                                      jointTrackingStateTypeID: 1,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 12,
                                          value: 11,
                                          name: "HandRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 2,
                                          value: 1,
                                          name: "Inferred"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 12,
                                      x: 0.168937862,
                                      y: -0.0221518986,
                                      z: 2.20635629,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 13,
                                          value: 12,
                                          name: "HipLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 13,
                                      x: 0.06395671,
                                      y: -0.4589712,
                                      z: 2.19039631,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 14,
                                          value: 13,
                                          name: "KneeLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 14,
                                      x: -0.0174252056,
                                      y: -0.8600426,
                                      z: 2.21588516,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 15,
                                          value: 14,
                                          name: "AnkleLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 15,
                                      x: -0.0455313772,
                                      y: -0.932893932,
                                      z: 2.12292624,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 16,
                                          value: 15,
                                          name: "FootLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 16,
                                      x: 0.336509138,
                                      y: -0.0256233476,
                                      z: 2.209118,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 17,
                                          value: 16,
                                          name: "HipRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 17,
                                      x: 0.419930249,
                                      y: -0.4560167,
                                      z: 2.20949268,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 18,
                                          value: 17,
                                          name: "KneeRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 18,
                                      x: 0.480411559,
                                      y: -0.868142664,
                                      z: 2.27347827,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 19,
                                          value: 18,
                                          name: "AnkleRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 19,
                                      x: 0.508531868,
                                      y: -0.930555463,
                                      z: 2.194128,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 20,
                                          value: 19,
                                          name: "FootRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 20,
                                      x: 0.2524937,
                                      y: 0.5727762,
                                      z: 2.37926722,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 21,
                                          value: 20,
                                          name: "SpineShoulder"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 21,
                                      x: -0.0516691245,
                                      y: 0.631339252,
                                      z: 2.231413,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 22,
                                          value: 21,
                                          name: "HandTipLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 22,
                                      x: -0.124825396,
                                      y: 0.6057363,
                                      z: 2.23985481,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 23,
                                          value: 22,
                                          name: "ThumbLeft"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 23,
                                      x: 0.9148579,
                                      y: 0.6280637,
                                      z: 2.35578537,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 24,
                                          value: 23,
                                          name: "HandTipRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    },
                                    {
                                      id: 0,
                                      jointTypeID: 24,
                                      x: 0.8275366,
                                      y: 0.6123315,
                                      z: 2.33977771,
                                      jointTrackingStateTypeID: 2,
                                      bodyDataFrameID: 0,
                                      details: {
                                        jointType: {
                                          id: 25,
                                          value: 24,
                                          name: "ThumbRight"
                                        },
                                        jointTrackingStateType: {
                                          id: 3,
                                          value: 2,
                                          name: "Tracked"
                                        }
                                      }
                                    }
                                  ]
                            }
                        },
                    ]
                       
                    }
   
                }
    ];
}