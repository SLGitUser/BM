using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;
using System.Collections.Generic;

namespace Bm.Models.Dp
{
    /// <summary>
    /// �ͻ���ϵ���
    /// </summary>
    [DisplayName("�ͻ���ϵ���")]
    public sealed class Customer : IId, IStamp
    {
        #region Implementation of IId

        /// <summary>
        /// ��ȡ�������ü�¼���
        /// </summary>
        /// <value>
        /// ��¼���
        /// </value>
        /// <remarks>
        /// ����洢ʱʹ���޷�������
        /// </remarks>
        [DisplayName("��¼���")]
        public long Id { get; set; }

        #endregion

        #region Implementation of ICreateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻���
        /// </remarks>
        [DisplayName("��¼������")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// ����ʹ�÷�����ʱ�䣬��Ҫʹ��Ĭ�Ͽ�ֵ
        /// </remarks>
        [DisplayName("��¼����ʱ��")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Implementation of IUpdateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻�����Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸���")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸�ʱ��")]
        public DateTime? UpdatedAt { get; set; }

        #endregion

        /// <summary>
        /// ��ȡ�������ÿͻ���ϵ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͻ���ϵ���")]
        [StringLength(49)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// ��ȡ�������õ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [StringLength(50)]
        [Required]
        public string CityNo { get; set; }

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������������ƴ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("����ƴ��")]
        [StringLength(40)]
        [Required]
        public string Pinyin { get; set; }

        /// <summary>
        /// ��ȡ���������Ա�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ա�")]
        [StringLength(5)]
        [Required]
        public string Gender { get; set; }

        /// <summary>
        /// ��ȡ��������֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("֤������")]
        [StringLength(49)]
        public string CardType { get; set; }

        /// <summary>
        /// ��ȡ��������֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("֤������")]
        [StringLength(30)]
        public string CardNo { get; set; }

        /// <summary>
        /// ��ȡ���������ֻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ֻ�����")]
        [StringLength(12)]
        public string Mobile { get; set; }

        /// <summary>
        /// ��ȡ�������õ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [StringLength(49)]
        public string Email { get; set; }

        /// <summary>
        /// ��ȡ��������ͷ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("ͷ��")]
        [StringLength(50)]
        public string Pic { get; set; }

        /// <summary>
        /// ��ȡ�������þ����˱��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����˱��")]
        [StringLength(49)]
        [Required]
        public string BrokerNo { get; set; }

        /// <summary>
        /// ��ȡ��������ע������
        /// </summary>
        /// <remark></remark>
        [DisplayName("ע������")]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// ��ȡ�������ñ�����������
        /// </summary>
        /// <remark></remark>
        [DisplayName("������������")]
        [Required]
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// ��ȡ�������ÿͻ��ȼ�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͻ��ȼ�")]
        [StringLength(1)]
        [Required]
        public string Level { get; set; }

        /// <summary>
        /// ��ȡ�������ÿͻ���ע
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͻ���ע")]
        [StringLength(50)]
        public string Remark
        { get; set; }
        public IList<Customer> Customers
        {
            get { return Customers; }
            set { Customers = value; }
        }

    }
}
