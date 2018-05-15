# Json API

## APIの基本
* 基本的なURL  
http://mc-tfserver/jenkins/

* XMLで取得する
<基本的なURL>XML

* Jsonで取得する
<基本的なURL>json

* その他APIの機能を使用する
<基本的なURL>json?<APIパラメータ>

## Jobの一覧を取得する。
http://mc-tfserver/jenkins/api/json?tree_jobs

* 取得できるリクエストデータ
```json:tree.json
    {  
    "_class":"hudson.model.Hudson",
    "assignedLabels":[  
        {  

        }
    ],
    "mode":"NORMAL",
    "nodeDescription":"ノード",
    "nodeName":"",
    "numExecutors":2,
    "description":null,
    "jobs":[  
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"LicenseDB_Backup",
            "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC11-MC1.App-develop-FxCop.ForInternal",
            "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop-FxCop.ForInternal/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC11-MC1.App-develop-R.ForInternal",
            "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop-R.ForInternal/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC11-MC1.App-develop.ForInternal",
            "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-develop.ForInternal/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC11-MC1.App-master.ForInternal",
            "url":"http://mc-tfserver/jenkins/job/MC11-MC1.App-master.ForInternal/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC14-MC4.BeOnePremium.AutoBuildAndTest",
            "url":"http://mc-tfserver/jenkins/job/MC14-MC4.BeOnePremium.AutoBuildAndTest/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC14-MC4.EcoDataPremium-develop.FxCop",
            "url":"http://mc-tfserver/jenkins/job/MC14-MC4.EcoDataPremium-develop.FxCop/",
            "color":"notbuilt"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC14-MC4.EcoDataPremiumRegist.AutoBuildAndTest",
            "url":"http://mc-tfserver/jenkins/job/MC14-MC4.EcoDataPremiumRegist.AutoBuildAndTest/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC14-MC4.Premium-d.Build.Test",
            "url":"http://mc-tfserver/jenkins/job/MC14-MC4.Premium-d.Build.Test/",
            "color":"blue"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"MC14-MC4.Premium.Setup",
            "url":"http://mc-tfserver/jenkins/job/MC14-MC4.Premium.Setup/",
            "color":"red"
        },
        {  
            "_class":"hudson.model.FreeStyleProject",
            "name":"Test.SetupBuild",
            "url":"http://mc-tfserver/jenkins/job/Test.SetupBuild/",
            "color":"blue"
        }
    ],
    "overallLoad":{  

    },
    "primaryView":{  
        "_class":"hudson.model.AllView",
        "name":"すべて",
        "url":"http://mc-tfserver/jenkins/"
    },
    "quietingDown":false,
    "slaveAgentPort":9000,
    "unlabeledLoad":{  
        "_class":"jenkins.model.UnlabeledLoadStatistics"
    },
    "useCrumbs":false,
    "useSecurity":true,
    "views":[  
        {  
            "_class":"hudson.model.ListView",
            "name":"EcoData",
            "url":"http://mc-tfserver/jenkins/view/EcoData/"
        },
        {  
            "_class":"hudson.model.AllView",
            "name":"すべて",
            "url":"http://mc-tfserver/jenkins/"
        },
        {  
            "_class":"hudson.model.ListView",
            "name":"定期実行ジョブ",
            "url":"http://mc-tfserver/jenkins/view/%E5%AE%9A%E6%9C%9F%E5%AE%9F%E8%A1%8C%E3%82%B8%E3%83%A7%E3%83%96/"
        },
        {  
            "_class":"hudson.model.ListView",
            "name":"訪問者管理システム",
            "url":"http://mc-tfserver/jenkins/view/%E8%A8%AA%E5%95%8F%E8%80%85%E7%AE%A1%E7%90%86%E3%82%B7%E3%82%B9%E3%83%86%E3%83%A0/"
        }
    ]
    }  

```

## 特定のジョブの情報を取得する。

```
http://mc-tfserver/jenkins/job/<Job名>/api/json
```
```
[例]
http://mc-tfserver/jenkins/job/LicenseDB_Backup/api/json
```

```
{  
   "_class":"hudson.model.FreeStyleProject",
   "actions":[  
      {  
         "_class":"hudson.plugins.disk_usage.ProjectDiskUsageAction"
      },
      {  

      },
      {  
         "_class":"hudson.plugins.jobConfigHistory.JobConfigHistoryProjectAction"
      },
      {  
         "_class":"com.cloudbees.plugins.credentials.ViewCredentialsAction"
      }
   ],
   "description":"ライセンス認証サーバー(microcircus.info)からDBファイルをバックアップするためのジョブです。\r\nこのジョブは毎日正午に実行します。\r\n(FTP経由でDBをダウンロードするのでルーターがONの時間でなければならない。)\r\n実行結果はバックアップフォルダの\"Log.txt\"に保存されます。\r\nバックアップフォルダは、\"\\\\D1-SERVER\\Bakup_LicenseData\" です。",
   "displayName":"LicenseDB_Backup",
   "displayNameOrNull":null,
   "name":"LicenseDB_Backup",
   "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/",
   "buildable":true,
   "builds":[  
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":13,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":12,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/12/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":11,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/11/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":10,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/10/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":9,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/9/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":7,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/7/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":6,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/6/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":5,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/5/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":4,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/4/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":3,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/3/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":2,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/2/"
      },
      {  
         "_class":"hudson.model.FreeStyleBuild",
         "number":1,
         "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/1/"
      }
   ],
   "color":"blue",
   "firstBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":1,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/1/"
   },
   "healthReport":[  
      {  
         "description":"ビルドの安定性: 最近の5個中、2個ビルドに失敗しました。",
         "iconClassName":"icon-health-40to59",
         "iconUrl":"health-40to59.png",
         "score":60
      }
   ],
   "inQueue":false,
   "keepDependencies":false,
   "lastBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":13,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/"
   },
   "lastCompletedBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":13,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/"
   },
   "lastFailedBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":10,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/10/"
   },
   "lastStableBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":13,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/"
   },
   "lastSuccessfulBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":13,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/"
   },
   "lastUnstableBuild":null,
   "lastUnsuccessfulBuild":{  
      "_class":"hudson.model.FreeStyleBuild",
      "number":10,
      "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/10/"
   },
   "nextBuildNumber":14,
   "property":[  
      {  
         "_class":"hudson.plugins.disk_usage.DiskUsageProperty"
      }
   ],
   "queueItem":null,
   "concurrentBuild":false,
   "downstreamProjects":[  

   ],
   "scm":{  
      "_class":"hudson.scm.NullSCM"
   },
   "upstreamProjects":[  

   ]
}

```

## 最終ビルド情報を取得する。
```
http://mc-tfserver/jenkins/job/<Job名>/lastBuild/api/xml
```
```
[例]
http://mc-tfserver/jenkins/job/LicenseDB_Backup/lastBuild/api/xml
```
```
{  
   "_class":"hudson.model.FreeStyleBuild",
   "actions":[  
      {  
         "_class":"hudson.model.CauseAction",
         "causes":[  
            {  
               "_class":"hudson.triggers.TimerTrigger$TimerTriggerCause",
               "shortDescription":"定期的に実行"
            }
         ]
      },
      {  

      },
      {  
         "_class":"hudson.plugins.disk_usage.BuildDiskUsageAction"
      },
      {  

      }
   ],
   "artifacts":[  

   ],
   "building":false,
   "description":null,
   "displayName":"#13",
   "duration":4997,
   "estimatedDuration":4037,
   "executor":null,
   "fullDisplayName":"LicenseDB_Backup #13",
   "id":"13",
   "keepLog":false,
   "number":13,
   "queueId":87,
   "result":"SUCCESS",
   "timestamp":1480650798370,
   "url":"http://mc-tfserver/jenkins/job/LicenseDB_Backup/13/",
   "builtOn":"",
   "changeSet":{  
      "_class":"hudson.scm.EmptyChangeLogSet",
      "items":[  

      ],
      "kind":null
   },
   "culprits":[  

   ]
}

```