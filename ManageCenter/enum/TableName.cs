using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageCenter
{
   /// <summary>
   /// 所有表名
   /// </summary>
    public enum TableName
    {
        bill_image,
        car_info,
        camera_info,
        car_trae_recod,
        commom_columns,
        company,
        config,
        material,
        material_category,
        material_taxation_recod,
        roles,
        station,
        table_sync,
        user,
        weighing_bill,
        bill_taxation_money_record,
    }

    public enum BillImageColumns
    {
        id,
        positon,
        address,
        bill_id,
        bill_number,
        type,
        is_up,
        up_datetime,
        remark,
        add_time,
        status,
        add_user_id,
        add_user_name,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        station_id,
        station_name,
    }
    /// <summary>
    ///车辆信息
    /// </summary>
    public enum CarInfoColumns
    {
        id,
        car_number,
        ic_number,
        driver,
        driver_mobile,
        driver_idnumber,
        affiliated_province,
        owner_id,
        owner_name,
        remark,
        add_time,
        status,
        add_user_id,
        add_user_name,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
    }

    /// <summary>
    ///车辆皮重调整记录表
    /// </summary>
    public enum CarTraeRecodColumns
    {
        id,
        car_number,
        weight,
        start_time,
        end_time,
        operator_id,
        operator_name,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }

    /// <summary>
    ///公司信息
    /// </summary>
    public enum CompanyColumns
    {
        id,
        name,
        legal_person,
        name_first_case,
        credit_number,
        businese_lincense_number,
        licese_esprise_time,
        abbreviation,
        abbreviation_first_case,
        level,
        parent_id,
        type,
        address,
        is_use_system,
        regester_type,
        customer_type,
        affiliated_province_id,
        affiliated_province,
        remark,
        add_time,
        status,
        add_user_id,
        add_user_name,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
    }

    /// <summary>
    ///配置表
    /// </summary>
    public enum ConfigColumns
    {
        id,
        config_name,
        config_value,
        config_type,
        description,
        station_id,
        station_name,
        remark,
        add_time,
        status,
        add_user_id,
        add_user_name,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
    }
    /// <summary>
    ///物质信息表
    /// </summary>
    public enum MaterialColumns
    {
        id,
        cate_id,
        cate_name,
        name,
        name_first_case,
        curr_taxation,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    /// <summary>
    ///物质分类表
    /// </summary>
    public enum MaterialCategoryColumns
    {
        id,
        name,
        name_first_case,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    /// <summary>
    ///物价税价调整记录表
    /// </summary>
    public enum MaterialTaxationRecodColumns
    {
        id,
        material_id,
        material_name,
        material_taxation,
        start_time,
        end_time,
        operator_id,
        operator_name,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    
    /// <summary>
    ///角色表
    /// </summary>
    public enum RolesColumns
    {
        id,
        name,
        role_level,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    /// <summary>
    ///站点信息表
    /// </summary>
    public enum StationColumns
    {
        id,
        name,
        name_first_case,
        privence,
        city,
        country,
        address,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    /// <summary>
    ///数据同步信息表
    /// </summary>
    public enum TableSyncColumns
    {
        id,
        table_name,
        sync_time,
        sync_count,
        has_up_status,
        no_sync,
        has_more,
        limit_count,
        is_commom,
    }
    /// <summary>
    ///人员信息表
    /// </summary>

    public enum UserColumns
    {
        id,
        name,
        name_first_case,
        id_number,
        phone,
        birthday,
        sex,
        role_id,
        role_name,
        role_level,
        pwd,
        station_id,
        station_level_type,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
    }
    /// <summary>
    ///过磅单
    /// </summary>
    public enum WeighingBillColumns
    {
        id,
        number,
        station_id,
        station_name,
        send_company,
        send_company_case,
        receive_company,
        material_cate_id,
        receive_company_case,
        material_cate_name,
        material_cate_case,
        material_id,
        material_name,
        material_first_case,
        material_taxation,
        car_number,
        driver,
        driver_id_umber,
        phone,
        send_gross_weight,
        send_trae_weight,
        send_net_weight,
        gross_weight,
        car_trae_weight,
        net_weight,
        limit_weight,
        overtop_weight,
        overtop_money,
        is_receive_money,
        operator_id,
        operator_name,
        check_user_id,
        check_user_name,
        is_up,
        up_datetime,
        remark,
        add_time,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
        status,
        add_user_id,
        add_user_name,
        is_checked,
        checked_time,
        print_frequency,
        print_Time,
        total_taxstion_money,
    }

    /// <summary>
    ///摄像头信息表
    /// </summary>

    public enum CameraInfoColumuns
    {
        id,
        ip,
        port,
        user_name,
        password,
        position,
        station_id,
        station_name,
        company_id,
        no_sync,
    }
    public enum BillTaxationMoneyRecordColumuns
    {
        id,
        money,
        receive_type,
        number,
        station_id,
        station_name,
        send_company,
        receive_company,
        material_id,
        material_name,
        car_number,
        driver,
        overtop_weight,
        material_taxation,
        remark,
        add_time,
        status,
        add_user_id,
        add_user_name,
        last_update_time,
        is_delete,
        delete_time,
        last_update_user_id,
        last_update_user_name,
    }
}
