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

