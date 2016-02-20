using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ���͹�˾
    /// </summary>
    [DisplayName("���͹�˾")]
    public sealed class BrokerageFirm : IId, IStamp
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


        #region ������Ϣ

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ�������þ��͹�˾����
        /// </summary>
        /// <remark></remark>
        [DisplayName("���͹�˾����")]
        [StringLength(50)]
        [Required]
        public string FirmNo { get; set; }

        /// <summary>
        /// ��ȡ�������õ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(20)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// ��ȡ�������õ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set; }
        /// <summary>
        /// ��ȡ����������֯��������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��֯��������")]
        [StringLength(20)]
        public string FirmOrgCode { get; set; }

        /// <summary>
        /// ��ȡ�������ù�˾��ַ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��˾��ַ")]
        [StringLength(20)]
        public string Address { get; set; }

        /// <summary>
        /// ��ȡ�������ù�˾ͼƬ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��˾ͼƬ")]
        [StringLength(50)]
        public string Pic { get; set; }
        #endregion
        #region ���˴�����Ϣ

        /// <summary>
        /// ��ȡ�������÷��˴�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˴�������")]
        [StringLength(50)]
        public string LegalName { get; set; }

        /// <summary>
        /// ��ȡ�������÷��˴���֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˴���֤������")]
        [StringLength(49)]
        public string LegalCardType { get; set; }

        /// <summary>
        /// ��ȡ�������÷��˴���֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˴���֤������")]
        [StringLength(50)]
        public string LegalCardNo { get; set; }

        /// <summary>
        /// ��ȡ�������÷��˴����ֻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˴����ֻ�����")]
        [StringLength(11)]
        public string LegalMobile { get; set; }

        /// <summary>
        /// ��ȡ�������÷��˴����������
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˴����������")]
        [StringLength(49)]
        public string LegalEmail { get; set; }

        #endregion
        #region ��ϵ����Ϣ

        /// <summary>
        /// ��ȡ����������ϵ������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ϵ������")]
        [StringLength(50)]
        public string ContactsName { get; set; }

        /// <summary>
        /// ��ȡ����������ϵ��֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ϵ��֤������")]
        [StringLength(49)]
        public string ContactsCardType { get; set; }

        /// <summary>
        /// ��ȡ����������ϵ��֤������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ϵ��֤������")]
        [StringLength(50)]
        public string ContactsCardNo { get; set; }

        /// <summary>
        /// ��ȡ����������ϵ���ֻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ϵ���ֻ�����")]
        [StringLength(11)]
        public string ContactsMobile { get; set; }

        /// <summary>
        /// ��ȡ����������ϵ�˵�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ϵ�˵�������")]
        [StringLength(49)]
        public string ContactsEmail { get; set; }

        #endregion

        #region ʱ��

        /// <summary>
        /// ��ȡ��������ע��ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("ע��ʱ��")]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// ��ȡ����������֤ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("��֤ʱ��")]
        public DateTime CheckAt { get; set; }

        #endregion
        #region ���

        /// <summary>
        /// ��ȡ�������ô���Ʊ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("����Ʊ��")]
        [Required]
        public decimal InvoiceTodoAmount { get; set; }

        /// <summary>
        /// ��ȡ���������ѿ�Ʊ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ѿ�Ʊ��")]
        [Required]
        public decimal InvoiceDoneAmount { get; set; }

        /// <summary>
        /// ��ȡ�������ÿ����ֶ�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����ֶ�")]
        [Required]
        public decimal CashTodoAmount { get; set; }

        /// <summary>
        /// ��ȡ���������������ֶ�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�������ֶ�")]
        [Required]
        public decimal CashDoingAmount { get; set; }

        /// <summary>
        /// ��ȡ�������������ֶ�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����ֶ�")]
        [Required]
        public decimal CashDoneAmount { get; set; }

        #endregion

    }
}
