ALTER TABLE APPS.BKLTMGR_LOGINHISTORY
 DROP PRIMARY KEY CASCADE;

DROP TABLE APPS.BKLTMGR_LOGINHISTORY CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_LOGINHISTORY
(
  LOGINID     INTEGER                           NOT NULL,
  STAFFID     VARCHAR2(15 BYTE)                 NOT NULL,
  LOGINDATE   DATE,
  LOGINTIME   VARCHAR2(15 BYTE),
  LOGOUTTIME  VARCHAR2(15 BYTE),
  LOGOUTDATE  DATE
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


DROP TABLE APPS.BKLTMGR_LOG_CREDIT CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_LOG_CREDIT
(
  LOGGID                INTEGER                 NOT NULL,
  REQUESTID             INTEGER                 NOT NULL,
  PAYMENTREF            VARCHAR2(50 BYTE)       NOT NULL,
  BATCHID               VARCHAR2(50 BYTE)       NOT NULL,
  INITIATINGBRANCH      VARCHAR2(5 BYTE),
  ACCOUNTNUMBER         VARCHAR2(15 BYTE),
  CREDIT                INTEGER                 NOT NULL,
  CURRENCY              VARCHAR2(3 BYTE),
  NARRATION             VARCHAR2(255 BYTE),
  TRANSACTIONSTARTDATE  DATE,
  TRANSACTIONENDDATE    DATE,
  STATUS                VARCHAR2(20 BYTE)       NOT NULL,
  GLCASAINDICATOR       VARCHAR2(10 BYTE),
  RESPONSECODE          VARCHAR2(5 BYTE),
  RESPONSEMSG           VARCHAR2(255 BYTE)
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


ALTER TABLE APPS.BKLTMGR_LOG_DEBIT
 DROP PRIMARY KEY CASCADE;

DROP TABLE APPS.BKLTMGR_LOG_DEBIT CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_LOG_DEBIT
(
  LOGGID                INTEGER                 NOT NULL,
  REQUESTID             INTEGER                 NOT NULL,
  PAYMENTREF            VARCHAR2(50 BYTE)       NOT NULL,
  BATCHID               VARCHAR2(50 BYTE)       NOT NULL,
  INITIATINGBRANCH      VARCHAR2(5 BYTE),
  ACCOUNTNUMBER         VARCHAR2(15 BYTE),
  DEBIT                 INTEGER                 NOT NULL,
  CURRENCY              VARCHAR2(3 BYTE),
  NARRATION             VARCHAR2(255 BYTE),
  TRANSACTIONSTARTDATE  DATE,
  TRANSACTIONENDDATE    DATE,
  STATUS                VARCHAR2(20 BYTE)       NOT NULL,
  GLCASAINDICATOR       VARCHAR2(10 BYTE),
  RESPONSECODE          VARCHAR2(5 BYTE),
  RESPONSEMSG           VARCHAR2(255 BYTE)
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


ALTER TABLE APPS.BKLTMGR_LSTISSDATE
 DROP PRIMARY KEY CASCADE;

DROP TABLE APPS.BKLTMGR_LSTISSDATE CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_LSTISSDATE
(
  "dateid"      INTEGER                         NOT NULL,
  "requestid"   INTEGER                         NOT NULL,
  "accountnum"  VARCHAR2(20 BYTE)               NOT NULL,
  "issuedate"   DATE                            NOT NULL
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


DROP TABLE APPS.BKLTMGR_NOTIFICATION CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_NOTIFICATION
(
  REQUESTID  INTEGER                            NOT NULL,
  SSA_STAT   INTEGER                            NOT NULL,
  HSSA_STAT  INTEGER                            NOT NULL
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


DROP TABLE APPS.BKLTMGR_REQUEST CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_REQUEST
(
  REQUESTID                INTEGER              NOT NULL,
  ACCOUNTNAME              VARCHAR2(100 BYTE)   NOT NULL,
  ACCOUNTNUMBER            VARCHAR2(15 BYTE)    NOT NULL,
  ACCOUNTBALANCE           INTEGER,
  SERIALNUMBERSTART        VARCHAR2(20 BYTE),
  SERIALNUMBEREND          VARCHAR2(20 BYTE),
  SSAID                    VARCHAR2(15 BYTE),
  SSALOGINID               INTEGER,
  HSSAID                   VARCHAR2(15 BYTE),
  HSSALOGINID              INTEGER,
  STATUS                   VARCHAR2(11 BYTE),
  HSSACOMMENT              VARCHAR2(255 BYTE),
  REQUESTCREATIONDATE      DATE,
  REQUESTCREATIONTIME      VARCHAR2(20 BYTE),
  REQUESTAUTORIZATIONTIME  VARCHAR2(20 BYTE),
  REQUESTAUTORIZATIONDATE  DATE,
  BRANCHCODE               VARCHAR2(6 BYTE),
  PHONENO                  VARCHAR2(16 BYTE)
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


ALTER TABLE APPS.BKLTMGR_ROLES
 DROP PRIMARY KEY CASCADE;

DROP TABLE APPS.BKLTMGR_ROLES CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_ROLES
(
  ROLEID     INTEGER,
  ROLENAME   VARCHAR2(15 BYTE),
  ROLE_DESC  VARCHAR2(150 BYTE)
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


DROP TABLE APPS.BKLTMGR_USERS CASCADE CONSTRAINTS;

CREATE TABLE APPS.BKLTMGR_USERS
(
  STAFFID     VARCHAR2(15 BYTE)                 NOT NULL,
  ROLEID      INTEGER                           NOT NULL,
  STAFFNAME   VARCHAR2(100 BYTE)                NOT NULL,
  USERNAME    VARCHAR2(20 BYTE),
  EMAIL       VARCHAR2(150 BYTE),
  BRANCHCODE  VARCHAR2(4 BYTE),
  BRANCHNAME  VARCHAR2(200 BYTE),
  STATUS      VARCHAR2(10 BYTE)
)
TABLESPACE FCUBS_APPS_DATA
RESULT_CACHE (MODE DEFAULT)
PCTUSED    0
PCTFREE    10
INITRANS   1
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


CREATE UNIQUE INDEX APPS.BKLTMGR_LOGINHISTORY_PK ON APPS.BKLTMGR_LOGINHISTORY
(LOGINID)
LOGGING
TABLESPACE FCUBS_APPS_DATA
PCTFREE    10
INITRANS   2
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
NOPARALLEL;


CREATE UNIQUE INDEX APPS.BKLTMGR_LOG_DEBIT_PK ON APPS.BKLTMGR_LOG_DEBIT
(LOGGID)
LOGGING
TABLESPACE FCUBS_APPS_DATA
PCTFREE    10
INITRANS   2
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
NOPARALLEL;


CREATE UNIQUE INDEX APPS.BKLTMGR_LSTISSDATE_PK ON APPS.BKLTMGR_LSTISSDATE
("dateid")
LOGGING
TABLESPACE FCUBS_APPS_DATA
PCTFREE    10
INITRANS   2
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
NOPARALLEL;


CREATE UNIQUE INDEX APPS.BKLTMGR_ROLES_PK ON APPS.BKLTMGR_ROLES
(ROLEID)
LOGGING
TABLESPACE FCUBS_APPS_DATA
PCTFREE    10
INITRANS   2
MAXTRANS   255
STORAGE    (
            INITIAL          64K
            NEXT             1M
            MINEXTENTS       1
            MAXEXTENTS       UNLIMITED
            PCTINCREASE      0
            BUFFER_POOL      DEFAULT
            FLASH_CACHE      DEFAULT
            CELL_FLASH_CACHE DEFAULT
           )
NOPARALLEL;


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOGINHISTORY_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOGINHISTORY
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGINID
  :new.LOGINID := BKLTMGR_LOGINHISTORY_SEQ.nextval;
END BKLTMGR_LOGINHISTORY_TRG;
/


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOG_CREDIT_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOG_CREDIT
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGGID
  :new.LOGGID := BKLTMGR_LOG_CREDIT_SEQ.nextval;
END BKLTMGR_LOG_CREDIT_TRG;
/


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOG_DEBIT_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOG_DEBIT
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGGID
  :new.LOGGID := BKLTMGR_LOG_DEBIT_SEQ.nextval;
END BKLTMGR_LOG_DEBIT_TRG;
/


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LSTISSDATE_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LSTISSDATE
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column dateid
  :new."dateid" := BKLTMGR_LSTISSDATE_SEQ.nextval;
END BKLTMGR_LSTISSDATE_TRG;
/


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_REQUEST_TRG
BEFORE INSERT
ON APPS.BKLTMGR_REQUEST
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column REQUESTID
  :new.REQUESTID := BKLTMGR_REQUEST_SEQ.nextval;
END BKLTMGR_REQUEST_TRG;
/


CREATE OR REPLACE TRIGGER APPS.BKLTMGR_ROLES_TRG
BEFORE INSERT
ON APPS.BKLTMGR_ROLES
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column ROLEID
  :new.ROLEID := BKLTMGR_ROLES_SEQ.nextval;
END BKLTMGR_ROLES_TRG;
/


ALTER TABLE APPS.BKLTMGR_LOGINHISTORY ADD (
  CONSTRAINT BKLTMGR_LOGINHISTORY_PK
  PRIMARY KEY
  (LOGINID)
  USING INDEX APPS.BKLTMGR_LOGINHISTORY_PK
  ENABLE VALIDATE);

ALTER TABLE APPS.BKLTMGR_LOG_DEBIT ADD (
  CONSTRAINT BKLTMGR_LOG_DEBIT_PK
  PRIMARY KEY
  (LOGGID)
  USING INDEX APPS.BKLTMGR_LOG_DEBIT_PK
  ENABLE VALIDATE);

ALTER TABLE APPS.BKLTMGR_LSTISSDATE ADD (
  CONSTRAINT BKLTMGR_LSTISSDATE_PK
  PRIMARY KEY
  ("dateid")
  USING INDEX APPS.BKLTMGR_LSTISSDATE_PK
  ENABLE VALIDATE);

ALTER TABLE APPS.BKLTMGR_ROLES ADD (
  CONSTRAINT BKLTMGR_ROLES_PK
  PRIMARY KEY
  (ROLEID)
  USING INDEX APPS.BKLTMGR_ROLES_PK
  ENABLE VALIDATE);

GRANT DELETE, INSERT, SELECT, UPDATE ON APPS.BKLTMGR_LOGINHISTORY TO BKLT_MGR;

GRANT DELETE, INSERT, SELECT, UPDATE ON APPS.BKLTMGR_REQUEST TO BKLT_MGR;

GRANT DELETE, INSERT, SELECT, UPDATE ON APPS.BKLTMGR_ROLES TO BKLT_MGR;

GRANT DELETE, INSERT, SELECT, UPDATE ON APPS.BKLTMGR_USERS TO BKLT_MGR;
DROP TRIGGER APPS.BKLTMGR_LOGINHISTORY_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOGINHISTORY_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOGINHISTORY
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGINID
  :new.LOGINID := BKLTMGR_LOGINHISTORY_SEQ.nextval;
END BKLTMGR_LOGINHISTORY_TRG;
/


DROP TRIGGER APPS.BKLTMGR_LOG_CREDIT_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOG_CREDIT_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOG_CREDIT
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGGID
  :new.LOGGID := BKLTMGR_LOG_CREDIT_SEQ.nextval;
END BKLTMGR_LOG_CREDIT_TRG;
/


DROP TRIGGER APPS.BKLTMGR_LOG_DEBIT_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LOG_DEBIT_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LOG_DEBIT
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column LOGGID
  :new.LOGGID := BKLTMGR_LOG_DEBIT_SEQ.nextval;
END BKLTMGR_LOG_DEBIT_TRG;
/


DROP TRIGGER APPS.BKLTMGR_LSTISSDATE_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_LSTISSDATE_TRG
BEFORE INSERT
ON APPS.BKLTMGR_LSTISSDATE
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column dateid
  :new."dateid" := BKLTMGR_LSTISSDATE_SEQ.nextval;
END BKLTMGR_LSTISSDATE_TRG;
/


DROP TRIGGER APPS.BKLTMGR_REQUEST_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_REQUEST_TRG
BEFORE INSERT
ON APPS.BKLTMGR_REQUEST
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column REQUESTID
  :new.REQUESTID := BKLTMGR_REQUEST_SEQ.nextval;
END BKLTMGR_REQUEST_TRG;
/


DROP TRIGGER APPS.BKLTMGR_ROLES_TRG;

CREATE OR REPLACE TRIGGER APPS.BKLTMGR_ROLES_TRG
BEFORE INSERT
ON APPS.BKLTMGR_ROLES
REFERENCING NEW AS New OLD AS Old
FOR EACH ROW
BEGIN
-- For Toad:  Highlight column ROLEID
  :new.ROLEID := BKLTMGR_ROLES_SEQ.nextval;
END BKLTMGR_ROLES_TRG;
/
DROP SEQUENCE APPS.BKLTMGR_LOGINHISTORY_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_LOGINHISTORY_SEQ
  START WITH 261
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;


DROP SEQUENCE APPS.BKLTMGR_LOG_CREDIT_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_LOG_CREDIT_SEQ
  START WITH 41
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;


DROP SEQUENCE APPS.BKLTMGR_LOG_DEBIT_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_LOG_DEBIT_SEQ
  START WITH 101
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;


DROP SEQUENCE APPS.BKLTMGR_LSTISSDATE_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_LSTISSDATE_SEQ
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;


DROP SEQUENCE APPS.BKLTMGR_REQUEST_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_REQUEST_SEQ
  START WITH 61
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;


DROP SEQUENCE APPS.BKLTMGR_ROLES_SEQ;

CREATE SEQUENCE APPS.BKLTMGR_ROLES_SEQ
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;
DROP PACKAGE FCUBSLIVE.BKLTMGR;

CREATE OR REPLACE PACKAGE FCUBSLIVE.BKLTMGR AS
/******************************************************************************
   NAME:       BKLTMGR
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        2/3/2017      scezeala       1. Created this package.
******************************************************************************/

  FUNCTION bklt_mgr_chk_user_exist(
            userid VARCHAR2,
            branchid VARCHAR2) 
            RETURN VARCHAR2;
  FUNCTION bklt_mgr_count_reqstat (
    branch_code      IN VARCHAR2,
    stats    IN VARCHAR2)
    RETURN VARCHAR2;
  FUNCTION bklt_mgr_getstatlogin(
    userid VARCHAR2
    ) 
    return varchar2;
  FUNCTION bklt_mgr_last_issue (
    account_num VARCHAR2
    )
    RETURN VARCHAR2;
  FUNCTION bklt_mgr_insert_request (
    acct_name      IN VARCHAR2,
    acct_number    IN VARCHAR2,
    acct_balance   IN DECIMAL,
    ssa_id         IN VARCHAR2,
    ssa_loginid    IN INTEGER,
    sn_no_strt     IN VARCHAR2,
    sn_no_end      IN VARCHAR2,
    req_date       IN VARCHAR2,
    req_time       IN VARCHAR2,
    req_status     IN VARCHAR2,
    branch_code    IN VARCHAR,
    phone_no    IN  VARCHAR2 )
    RETURN VARCHAR2;
    FUNCTION bklt_mgr_last_loginid (
    staff_id VARCHAR2
    )
    RETURN VARCHAR2;
    FUNCTION bklt_mgr_report (
    branch_code VARCHAR2,
    stats VARCHAR2,
    start_date VARCHAR2,
    end_date Varchar2)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_reqst_details (
    req_id INTEGER,
    branch_code varchar2)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_search_reqst (
    start_date VARCHAR2,
    end_date Varchar2,
    branch_code varchar2,
    reqst_status varchar2)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_select_all_roles 
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_select_all_users (
    branch_code VARCHAR2)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_select_all_users_a 
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_select_edit_users (
    userid VARCHAR2)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_select_user_roles(userid VARCHAR2) 
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_sel_rol_edit_users (
    role_id INTEGER)
    RETURN VARCHAR2;
    FUNCTION bklt_mgr_ssa_cust_det (
    req_id INTEGER)
    RETURN SYS_REFCURSOR;
    FUNCTION bklt_mgr_validateadminlogin1 (
    v_staffid     VARCHAR2,
    v_username    VARCHAR2)
    RETURN VARCHAR2;
    FUNCTION bklt_mgr_update_request (
    reqst_id      IN INTEGER,
    ssa_id         IN VARCHAR2,
    req_auth_date       IN VARCHAR2,
    req_auth_time       IN VARCHAR2,
    req_status     IN VARCHAR2,
    hssalogin_id IN INTEGER,
    hssastaff_id IN VARCHAR2,
    branch_code    IN VARCHAR2,
    hssa_comment    IN VARCHAR2)
    RETURN VARCHAR2;
  PROCEDURE bklt_mgr_insert_crdt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    BATCH_ID   IN VARCHAR2,
    INITIATING_BRANCH         IN VARCHAR2,
    ACCOUNT_NUMBER    IN VARCHAR2,
    CREDITS     IN DECIMAL,
    CURRENCYS      IN VARCHAR2,
    NARRATIONS       IN VARCHAR2,
    TRANSACTION_STARTDATE       IN DATE,
    STAT    IN VARCHAR,
    GLCASA_INDICATOR    IN  VARCHAR2 );
  PROCEDURE bklt_mgr_insert_dbt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    BATCH_ID   IN VARCHAR2,
    INITIATING_BRANCH         IN VARCHAR2,
    ACCOUNT_NUMBER    IN VARCHAR2,
    DEBITS     IN DECIMAL,
    CURRENCYS      IN VARCHAR2,
    NARRATIONS       IN VARCHAR2,
    TRANSACTION_STARTDATE       IN DATE,
    STAT    IN VARCHAR,
    GLCASA_INDICATOR    IN  VARCHAR2 );
    PROCEDURE bklt_mgr_insert_request_2(
        acct_name in VARCHAR2,
        acct_number in varchar2,
        acct_balance in decimal,
        ssa_id in varchar2,
        ssa_loginid in integer,
        sn_no_strt in varchar2,
        sn_no_end in varchar2,
        req_date in varchar2,
        req_time in varchar2,
        req_status in varchar2
        );
   PROCEDURE bklt_mgr_insert_testing2(
        acct_name in VARCHAR2,
        acct_number in varchar2,
        acct_balance in decimal,
        ssa_id in varchar2,
        ssa_loginid in integer,
        sn_no_strt in varchar2,
        sn_no_end in varchar2,
        req_date in varchar2,
        req_time in varchar2,
        req_status in varchar2,
        out_request out integer
        );
   PROCEDURE bklt_mgr_insert_user(
        userid in VARCHAR2,
        user_role in integer,
        staff_name in varchar2,
        user_name in varchar2,
        user_email in varchar2,
        branch_code in varchar2,
        branch_name in varchar2,
        user_status in varchar2
        ); 
  PROCEDURE bklt_mgr_log_login_users (
    staff_id      IN VARCHAR2,
    login_date    IN VARCHAR2,
    login_time    IN VARCHAR2
    );
  PROCEDURE bklt_mgr_log_logout_users (
    login_id      IN INTEGER,
    logout_date    IN VARCHAR2,
    logout_time    IN VARCHAR2
    );
  PROCEDURE bklt_mgr_update_crdt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    TRANSACTION_ENDDATE     IN DATE,
    STAT    IN VARCHAR2,
    RESPONSE_CODE IN VARCHAR2,
    RESPONSE_MSG IN VARCHAR2);
  PROCEDURE bklt_mgr_update_dbt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    TRANSACTION_ENDDATE     IN DATE,
    STAT    IN VARCHAR,
    RESPONSE_CODE IN VARCHAR2,
    RESPONSE_MSG IN VARCHAR2);
  PROCEDURE bklt_mgr_update_user_role(
    userid VARCHAR2,
    role_id INTEGER
    );
  PROCEDURE bklt_mgr_update_user_stat(
    userid VARCHAR2,
    status_val VARCHAR2
    );
  PROCEDURE bklt_mgr_validateadminlogin (
    v_staffid    IN     VARCHAR2,
    v_username   IN     VARCHAR2,
    o_outdata       OUT VARCHAR2);  

END BKLTMGR;
/
DROP PACKAGE BODY FCUBSLIVE.BKLTMGR;

CREATE OR REPLACE PACKAGE BODY FCUBSLIVE.BKLTMGR AS
/******************************************************************************
   NAME:       BKLTMGR
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        2/3/2017      scezeala       1. Created this package.
******************************************************************************/

  FUNCTION bklt_mgr_chk_user_exist(
            userid VARCHAR2,
            branchid VARCHAR2) 
            RETURN VARCHAR2
            IS
o_count   VARCHAR2(2);
BEGIN
    select count(*)
      INTO o_count
from apps.bkltmgr_users where staffid = userid and branchcode = branchid;
RETURN o_count;
END;
  FUNCTION bklt_mgr_count_reqstat (
    branch_code      IN VARCHAR2,
    stats    IN VARCHAR2)
    RETURN VARCHAR2
    IS
    total_count   VARCHAR2(255);
BEGIN
select count(*) into total_count 
from apps.bkltmgr_request where status = stats  and branchcode = branch_code;
RETURN total_count;
END;
  FUNCTION bklt_mgr_getstatlogin(
    userid VARCHAR2
    ) 
    return varchar2
    is
user_stat varchar2(10);
begin
select status into user_stat from APPS.BKLTMGR_USERS where staffid = userid ;
return user_stat;
end;
  FUNCTION bklt_mgr_last_issue (
    account_num VARCHAR2
    )
    RETURN VARCHAR2
    IS
    date_approved   VARCHAR2(150);
BEGIN    
  SELECT * INTO date_approved
  FROM (SELECT "issuedate" FROM APPS.BKLTMGR_LSTISSDATE WHERE ("accountnum" = account_num
    ) ORDER BY "dateid" DESC )
 WHERE ROWNUM = 1;      

    RETURN date_approved;
END;
  FUNCTION bklt_mgr_insert_request (
    acct_name      IN VARCHAR2,
    acct_number    IN VARCHAR2,
    acct_balance   IN DECIMAL,
    ssa_id         IN VARCHAR2,
    ssa_loginid    IN INTEGER,
    sn_no_strt     IN VARCHAR2,
    sn_no_end      IN VARCHAR2,
    req_date       IN VARCHAR2,
    req_time       IN VARCHAR2,
    req_status     IN VARCHAR2,
    branch_code    IN VARCHAR,
    phone_no    IN  VARCHAR2 )
    RETURN VARCHAR2
    IS
    out_request   VARCHAR2(100);
BEGIN
    INSERT INTO apps.bkltmgr_request (accountname,
                                      accountnumber,
                                      accountbalance,
                                      serialnumberstart,
                                      serialnumberend,
                                      ssaid,
                                      ssaloginid,
                                      status,
                                      requestcreationdate,
                                     requestcreationtime,
                                     branchcode,
                                     phoneno)
    VALUES (acct_name,
            acct_number,
            acct_balance,
            sn_no_strt,
            sn_no_end,
            ssa_id,
            ssa_loginid,
            req_status,
            TO_DATE (req_date, 'MM/DD/YYYY'),
            req_time,
            branch_code,
            phone_no);

    COMMIT;
    
    select email INTO out_request from apps.bkltmgr_users usr, apps.bkltmgr_roles rls where usr.roleid = RLS.ROLEID
AND RLS.ROLENAME = 'Checker' AND USR.BRANCHCODE = branch_code;

    RETURN out_request;
END;
    FUNCTION bklt_mgr_last_loginid (
    staff_id VARCHAR2
    )
    RETURN VARCHAR2
    IS
    last_id   VARCHAR2(150);
BEGIN    
      SELECT 
   LOGINID INTO last_id
    FROM APPS.BKLTMGR_LOGINHISTORY WHERE ROWNUM = 1 AND STAFFID = staff_id
    ORDER BY LOGINID DESC ;       

    RETURN last_id;
END;
    FUNCTION bklt_mgr_report (
    branch_code VARCHAR2,
    stats VARCHAR2,
    start_date VARCHAR2,
    end_date Varchar2)
    RETURN SYS_REFCURSOR
    IS
    report_list   SYS_REFCURSOR;
BEGIN
    OPEN report_list FOR  
select req.requestcreationdate,req.accountname,req.accountnumber,lst."issuedate",req.serialnumberstart,req.serialnumberend,req.ssaid,req.hssaid,
req.requestcreationtime,req.requestautorizationtime from apps.bkltmgr_request req, apps.bkltmgr_lstissdate lst where 
req.REQUESTCREATIONDATE BETWEEN TO_DATE(start_date,'MM/DD/YYYY') AND TO_DATE(end_date,'MM/DD/YYYY') AND
req.requestid = lst."requestid" AND
req.status = stats AND
REQ.BRANCHCODE = branch_code;

    RETURN report_list;
END;
    FUNCTION bklt_mgr_reqst_details (
    req_id INTEGER,
    branch_code varchar2)
    RETURN SYS_REFCURSOR
    IS
    request_list   SYS_REFCURSOR;
BEGIN
    OPEN request_list FOR
        SELECT 
   REQUESTID, ACCOUNTNAME, ACCOUNTNUMBER, ACCOUNTBALANCE, 
   SSAID,APPS.BKLTMGR_REQUEST.SERIALNUMBERSTART,APPS.BKLTMGR_REQUEST.SERIALNUMBEREND,APPS.BKLTMGR_REQUEST.PHONENO,
   APPS.BKLTMGR_REQUEST.REQUESTCREATIONDATE,APPS.BKLTMGR_REQUEST.REQUESTCREATIONTIME,APPS.BKLTMGR_REQUEST.REQUESTAUTORIZATIONDATE,
   APPS.BKLTMGR_REQUEST.REQUESTAUTORIZATIONTIME,APPS.BKLTMGR_REQUEST.STATUS,APPS.BKLTMGR_REQUEST.HSSACOMMENT 
   FROM APPS.BKLTMGR_REQUEST JOIN APPS.BKLTMGR_USERS ON APPS.BKLTMGR_REQUEST.SSAID = APPS.BKLTMGR_USERS.STAFFID
WHERE REQUESTID = req_id 
AND APPS.BKLTMGR_REQUEST.BRANCHCODE = branch_code;

    RETURN request_list;
END;
    FUNCTION bklt_mgr_search_reqst (
    start_date VARCHAR2,
    end_date Varchar2,
    branch_code varchar2,
    reqst_status varchar2)
    RETURN SYS_REFCURSOR
    IS
    request_list   SYS_REFCURSOR;
BEGIN
    OPEN request_list FOR
        SELECT 
   REQUESTID, ACCOUNTNAME, ACCOUNTNUMBER, 
   SSAID,APPS.BKLTMGR_USERS.STAFFNAME,
   APPS.BKLTMGR_REQUEST.STATUS,  
   REQUESTCREATIONDATE, REQUESTCREATIONTIME
FROM APPS.BKLTMGR_REQUEST JOIN APPS.BKLTMGR_USERS ON APPS.BKLTMGR_REQUEST.SSAID = APPS.BKLTMGR_USERS.STAFFID
WHERE REQUESTCREATIONDATE   BETWEEN TO_DATE(start_date,'MM/DD/YYYY') AND TO_DATE(end_date,'MM/DD/YYYY') 
AND APPS.BKLTMGR_REQUEST.BRANCHCODE = branch_code AND APPS.BKLTMGR_REQUEST.STATUS = reqst_status ;

    RETURN request_list;
END;
    FUNCTION bklt_mgr_select_all_roles 
    RETURN SYS_REFCURSOR
    IS
    roles_list   SYS_REFCURSOR;
BEGIN
    OPEN roles_list FOR
        SELECT * from APPS.BKLTMGR_ROLES ;

    RETURN roles_list;
END;
    FUNCTION bklt_mgr_select_all_users (
    branch_code VARCHAR2)
    RETURN SYS_REFCURSOR
    IS
    users_list   SYS_REFCURSOR;
BEGIN
    OPEN users_list FOR
        SELECT apps.bkltmgr_users.staffname,
               apps.bkltmgr_users.staffid,
               apps.bkltmgr_users.email,
               apps.bkltmgr_users.branchname,
               apps.bkltmgr_users.branchcode,
               apps.bkltmgr_users.status,
               apps.bkltmgr_roles.role_desc,
               apps.bkltmgr_roles.rolename
          FROM     apps.bkltmgr_users
               JOIN
                   apps.bkltmgr_roles
               ON apps.bkltmgr_users.roleid = apps.bkltmgr_roles.roleid
         WHERE apps.bkltmgr_users.branchcode = branch_code;

    RETURN users_list;
END;
    FUNCTION bklt_mgr_select_all_users_a 
    RETURN SYS_REFCURSOR
    IS
    users_list   SYS_REFCURSOR;
BEGIN
    OPEN users_list FOR
        SELECT apps.bkltmgr_users.staffname,
               apps.bkltmgr_users.staffid,
               apps.bkltmgr_users.email,
               apps.bkltmgr_users.branchname,
               apps.bkltmgr_users.branchcode,
               apps.bkltmgr_users.status,
               apps.bkltmgr_roles.role_desc,
               apps.bkltmgr_roles.rolename
          FROM     apps.bkltmgr_users
               JOIN
                   apps.bkltmgr_roles
               ON apps.bkltmgr_users.roleid = apps.bkltmgr_roles.roleid;
         RETURN users_list;
END;
    FUNCTION bklt_mgr_select_edit_users (
    userid VARCHAR2)
    RETURN SYS_REFCURSOR
    IS
    users_list   SYS_REFCURSOR;
BEGIN
    OPEN users_list FOR
        SELECT apps.bkltmgr_users.staffname,
               APPS.BKLTMGR_USERS.USERNAME,
               apps.bkltmgr_users.staffid,
               apps.bkltmgr_users.email,
               apps.bkltmgr_users.status,
               apps.bkltmgr_roles.role_desc,
               apps.bkltmgr_roles.rolename
          FROM     apps.bkltmgr_users
               JOIN
                   apps.bkltmgr_roles
               ON apps.bkltmgr_users.roleid = apps.bkltmgr_roles.roleid
         WHERE apps.bkltmgr_users.staffid = userid;

    RETURN users_list;
END;
    FUNCTION bklt_mgr_select_user_roles(userid VARCHAR2) 
    RETURN SYS_REFCURSOR
    IS
    roles_list   SYS_REFCURSOR;
BEGIN
    OPEN roles_list FOR
        SELECT apps.bkltmgr_roles.roleid, apps.bkltmgr_roles.rolename from APPS.BKLTMGR_ROLES JOIN APPS.BKLTMGR_USERS ON APPS.BKLTMGR_ROLES.ROLEID = APPS.BKLTMGR_USERS.ROLEID
WHERE APPS.BKLTMGR_USERS.STAFFID  = userid;
    RETURN roles_list;
END;
    FUNCTION bklt_mgr_sel_rol_edit_users (
    role_id INTEGER)
    RETURN VARCHAR2
    IS
    user_role   VARCHAR2(150);
BEGIN
    SELECT 
            APPS.BKLTMGR_ROLES.ROLE_DESC
            INTO user_role
          FROM     apps.bkltmgr_roles
               
         WHERE APPS.BKLTMGR_ROLES.ROLEID = role_id;

    RETURN user_role;
END;
    FUNCTION bklt_mgr_ssa_cust_det (
    req_id INTEGER)
    RETURN SYS_REFCURSOR
    IS
    users_list   SYS_REFCURSOR;
BEGIN
    OPEN users_list FOR
select req.accountname, req.accountnumber, req.hssacomment,usr.staffname,usr.email from apps.bkltmgr_request req, 
apps.bkltmgr_users usr where req.ssaid = usr.staffid and req.requestid = req_id;

    RETURN users_list;
END;
    FUNCTION bklt_mgr_validateadminlogin1 (
    v_staffid     VARCHAR2,
    v_username    VARCHAR2)
    RETURN VARCHAR2
    IS
    o_outdata   VARCHAR2 (100);
BEGIN
    SELECT staffid
      INTO o_outdata
      FROM apps.bkltmgr_users bks, apps.bkltmgr_roles bkb
     WHERE     bks.roleid = bkb.roleid
           AND bks.staffid = v_staffid                             --'5429202'
           AND bks.username = v_username                          --'scezeala'
           AND bks.roleid = 1;

    RETURN o_outdata;
END;
    FUNCTION bklt_mgr_update_request (
    reqst_id      IN INTEGER,
    ssa_id         IN VARCHAR2,
    req_auth_date       IN VARCHAR2,
    req_auth_time       IN VARCHAR2,
    req_status     IN VARCHAR2,
    hssalogin_id IN INTEGER,
    hssastaff_id IN VARCHAR2,
    branch_code    IN VARCHAR2,
    hssa_comment    IN VARCHAR2)
    RETURN VARCHAR2
    IS
    out_request   VARCHAR2(200);
    acct_num    VARCHAR2(20);
BEGIN
    UPDATE apps.bkltmgr_request SET REQUESTAUTORIZATIONTIME = req_auth_time,
       REQUESTAUTORIZATIONDATE = TO_DATE (req_auth_date, 'MM/DD/YYYY'),
       STATUS = req_status, HSSACOMMENT = hssa_comment, HSSAID = hssastaff_id,
       HSSALOGINID = hssalogin_id 
       WHERE REQUESTID = reqst_id AND BRANCHCODE = branch_code
       AND SSAID = ssa_id ;   
    COMMIT;

    SELECT email
      INTO out_request
      FROM apps.bkltmgr_users
     where staffid = ssa_id and branchcode = branch_code;
     
     SELECT accountnumber INTO acct_num FROM apps.bkltmgr_request WHERE REQUESTID = reqst_id;
     
     INSERT INTO APPS.BKLTMGR_LSTISSDATE ("requestid","accountnum","issuedate") 
     VALUES (reqst_id,acct_num,TO_DATE (req_auth_date, 'MM/DD/YYYY'));
     
     UPDATE apps.bkltmgr_notification SET hssa_stat = '1' WHERE requestid = reqst_id;

    RETURN out_request;
END;
  PROCEDURE bklt_mgr_insert_crdt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    BATCH_ID   IN VARCHAR2,
    INITIATING_BRANCH         IN VARCHAR2,
    ACCOUNT_NUMBER    IN VARCHAR2,
    CREDITS     IN DECIMAL,
    CURRENCYS      IN VARCHAR2,
    NARRATIONS       IN VARCHAR2,
    TRANSACTION_STARTDATE       IN DATE,
    STAT    IN VARCHAR,
    GLCASA_INDICATOR    IN  VARCHAR2 )
    IS  
BEGIN
    INSERT INTO apps.bkltmgr_log_credit (REQUESTID,             
                                      PAYMENTREF,            
                                      BATCHID,               
                                      INITIATINGBRANCH,      
                                      ACCOUNTNUMBER,         
                                      CREDIT,                
                                      CURRENCY,              
                                      NARRATION,             
                                      TRANSACTIONSTARTDATE,  
                                      STATUS,                
                                      GLCASAINDICATOR )
    VALUES (REQUEST_ID,
            PAYMENT_REF,
            BATCH_ID,
            INITIATING_BRANCH,
            ACCOUNT_NUMBER,
            CREDITS,
            CURRENCYS,
            NARRATIONS,
            TRANSACTION_STARTDATE,
            STAT,
            GLCASA_INDICATOR);

    COMMIT;

END;
  PROCEDURE bklt_mgr_insert_dbt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    BATCH_ID   IN VARCHAR2,
    INITIATING_BRANCH         IN VARCHAR2,
    ACCOUNT_NUMBER    IN VARCHAR2,
    DEBITS     IN DECIMAL,
    CURRENCYS      IN VARCHAR2,
    NARRATIONS       IN VARCHAR2,
    TRANSACTION_STARTDATE       IN DATE,
    STAT    IN VARCHAR,
    GLCASA_INDICATOR    IN  VARCHAR2 )
    IS  
BEGIN
    INSERT INTO apps.bkltmgr_log_debit (REQUESTID,             
                                      PAYMENTREF,            
                                      BATCHID,               
                                      INITIATINGBRANCH,      
                                      ACCOUNTNUMBER,         
                                      DEBIT,                
                                      CURRENCY,              
                                      NARRATION,             
                                      TRANSACTIONSTARTDATE,  
                                      STATUS,                
                                      GLCASAINDICATOR )
    VALUES (REQUEST_ID,
            PAYMENT_REF,
            BATCH_ID,
            INITIATING_BRANCH,
            ACCOUNT_NUMBER,
            DEBITS,
            CURRENCYS,
            NARRATIONS,
            TRANSACTION_STARTDATE,
            STAT,
            GLCASA_INDICATOR);

    COMMIT;

END;
    PROCEDURE bklt_mgr_insert_request_2(
        acct_name in VARCHAR2,
        acct_number in varchar2,
        acct_balance in decimal,
        ssa_id in varchar2,
        ssa_loginid in integer,
        sn_no_strt in varchar2,
        sn_no_end in varchar2,
        req_date in varchar2,
        req_time in varchar2,
        req_status in varchar2
        )
        is 
begin
insert into APPS.BKLTMGR_REQUEST 
    (ACCOUNTNAME,ACCOUNTNUMBER,ACCOUNTBALANCE, SERIALNUMBERSTART,SERIALNUMBEREND,SSAID,SSALOGINID,STATUS,REQUESTCREATIONDATE,REQUESTCREATIONTIME)
values (acct_name,acct_number,acct_balance,sn_no_strt,sn_no_end,ssa_id,ssa_loginid,req_status,TO_DATE (req_date, 'MM/DD/YYYY'),req_time);

end;
   PROCEDURE bklt_mgr_insert_testing2(
        acct_name in VARCHAR2,
        acct_number in varchar2,
        acct_balance in decimal,
        ssa_id in varchar2,
        ssa_loginid in integer,
        sn_no_strt in varchar2,
        sn_no_end in varchar2,
        req_date in varchar2,
        req_time in varchar2,
        req_status in varchar2,
        out_request out integer
        )
        as
begin
insert into APPS.BKLTMGR_REQUEST 
    (ACCOUNTNAME,ACCOUNTNUMBER,ACCOUNTBALANCE, SERIALNUMBERSTART,SERIALNUMBEREND,SSAID,SSALOGINID,STATUS,REQUESTCREATIONDATE,REQUESTCREATIONTIME)
values (acct_name,acct_number,acct_balance,sn_no_strt,sn_no_end,ssa_id,ssa_loginid,req_status,TO_DATE (req_date, 'MM/DD/YYYY'),req_time);

select max(requestid) into out_request from APPS.BKLTMGR_REQUEST WHERE ssaid = ssa_id AND ssaloginid = ssa_loginid;
commit;
end;
   PROCEDURE bklt_mgr_insert_user(
        userid in VARCHAR2,
        user_role in integer,
        staff_name in varchar2,
        user_name in varchar2,
        user_email in varchar2,
        branch_code in varchar2,
        branch_name in varchar2,
        user_status in varchar2
        )
        is
begin
insert into apps.bkltmgr_users 
    (staffid,roleid,staffname, username,email,branchcode,branchname,status )
values (userid,user_role,staff_name,user_name,user_email,branch_code,branch_name,user_status);

end; 
  PROCEDURE bklt_mgr_log_login_users (
    staff_id      IN VARCHAR2,
    login_date    IN VARCHAR2,
    login_time    IN VARCHAR2
    )
    AS
BEGIN
    INSERT INTO apps.bkltmgr_loginhistory (staffid,
                                           logindate,
                                           logintime
                                           )
    VALUES (staff_id,
            TO_DATE (login_date, 'MM/DD/YYYY'),
            login_time
            );
END;
  PROCEDURE bklt_mgr_log_logout_users (
    login_id      IN INTEGER,
    logout_date    IN VARCHAR2,
    logout_time    IN VARCHAR2
    )
    AS
BEGIN
    UPDATE apps.bkltmgr_loginhistory 
        SET
    logouttime = logout_time,
    logoutdate = TO_DATE(logout_date,'MM/DD/YYYY')
   WHERE loginid = login_id;
END;
  PROCEDURE bklt_mgr_update_crdt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    TRANSACTION_ENDDATE     IN DATE,
    STAT    IN VARCHAR2,
    RESPONSE_CODE IN VARCHAR2,
    RESPONSE_MSG IN VARCHAR2)
       IS  
BEGIN
    UPDATE apps.bkltmgr_log_credit SET   
          TRANSACTIONENDDATE = TRANSACTION_ENDDATE,     
          STATUS = STAT, RESPONSECODE = RESPONSE_CODE, RESPONSEMSG = RESPONSE_MSG               
          WHERE REQUESTID = REQUEST_ID AND PAYMENTREF = PAYMENT_REF;
    

END;
  PROCEDURE bklt_mgr_update_dbt_log (
    REQUEST_ID      IN INTEGER,
    PAYMENT_REF    IN VARCHAR2,
    TRANSACTION_ENDDATE     IN DATE,
    STAT    IN VARCHAR,
    RESPONSE_CODE IN VARCHAR2,
    RESPONSE_MSG IN VARCHAR2)
    IS  
BEGIN
    UPDATE apps.bkltmgr_log_debit SET   
          TRANSACTIONENDDATE = TRANSACTION_ENDDATE,     
          STATUS = STAT,RESPONSECODE = RESPONSE_CODE, RESPONSEMSG = RESPONSE_MSG                
          WHERE REQUESTID = REQUEST_ID AND PAYMENTREF = PAYMENT_REF;
    
END;
  PROCEDURE bklt_mgr_update_user_role(
    userid VARCHAR2,
    role_id INTEGER
    )
    is
begin
update apps.bkltmgr_users set roleid = role_id where staffid = userid; 

end;
  PROCEDURE bklt_mgr_update_user_stat(
    userid VARCHAR2,
    status_val VARCHAR2
    )
    is
begin
update apps.bkltmgr_users set status = status_val where staffid = userid; 

end;
  PROCEDURE bklt_mgr_validateadminlogin (
    v_staffid    IN     VARCHAR2,
    v_username   IN     VARCHAR2,
    o_outdata       OUT VARCHAR2)
IS
BEGIN
    SELECT staffid
      INTO o_outdata
      FROM     apps.bkltmgr_users
           JOIN
               apps.bkltmgr_roles
           ON apps.bkltmgr_users.roleid = apps.bkltmgr_roles.roleid
     WHERE     apps.bkltmgr_users.staffid = v_staffid
           AND apps.bkltmgr_users.username = v_username
           AND apps.bkltmgr_users.roleid = 1;
END;  

END BKLTMGR;
/
