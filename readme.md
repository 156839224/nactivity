## ������ϵͳ���ݿ�����
* MySql�������ݿ�schema activiti
* �޸����ݿ����� appsettings.json
```
"WorkflowDataSource"��{
    "providerName": "MySql",
    "database": "activiti",��//���ݿ�����
    "connectionString": "server=localhost;database=���ݿ�����;uid=�û���;pwd=����;Character Set=utf8"
}
```

## ��ʼ�����ݿ��ṹ
* �״������޸���ĿĿ¼�ļ� 
* ����ʹ���޸����÷�ʽ��resources\activiti.cfg.json ����
  * databaseSchemaUpdate����˵��: 
    *  update��true �����ݿ��ṹ�����仯ʱ����Ϊtrue��ϵͳ���Զ����µ��µı�ṹ
    *  drop-create��ϵͳ����ɾ�����ٴ����������ý��ڿ���ʱ���ã�nuget�����øù��ܲ�����
    *  create: �����ý������ݿ�Ϊ��ʱ������ṹ������ϵͳ���޷��������״����������޸ĸ�����Ϊfalse�������ٴ����������޷�������
*  
  ```
  {
    "id": "processEngineConfiguration",
    "type": "org.activiti.engine.impl.cfg.StandaloneProcessEngineConfiguration",
    //web����Ӧ�ó�������
    "applicationName": "workflow",
    //���ݿ���²��ԣ�true=update drop-create create
    "databaseSchemaUpdate": "���ݿ���²���",
    ...
  ```

* �ֹ��������ݿ��
  * ������
    * ��������ʱ��ṹ�� resources\db\create\activiti.mysql.create.engine.sql  
    * ������ʷ��¼��ṹ��resources\db\create\activiti.mysql.create.history.sql
  * ɾ����
    * ��������ʱ��ṹ�� resources\db\drop\activiti.mysql.drop.engine.sql  
    * ������ʷ��¼��ṹ��resources\db\drop\activiti.mysql.drop.history.sql

## �������̱༭�����ʵ�ַ http://localhost:11015/index.html
* �༭������ BpmnEditor