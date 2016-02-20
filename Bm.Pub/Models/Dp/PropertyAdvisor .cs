using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ��ҵ����
    /// </summary>
    [DisplayName("��ҵ����")]
    public sealed class PropertyAdvisor : IId, IStamp
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
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ�������ñ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("���")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }

        /// <summary>
        /// ��ȡ���������Ա�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ա�")]
        [StringLength(5)]
        public string Gender { get; set; }

        /// <summary>
        /// ��ȡ���������ֻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ֻ�����")]
        [StringLength(12)]
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// ��ȡ�������õ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [StringLength(49)]
        [Required]
        public string Email { get; set; }

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
        /// ��ȡ�������þ��͹�˾
        /// </summary>
        /// <remark></remark>
        [DisplayName("���͹�˾")]
        [StringLength(50)]
        public string Firm { get; set; }

        /// <summary>
        /// ��ȡ�������þ��͹�˾����
        /// </summary>
        /// <remark></remark>
        [DisplayName("���͹�˾����")]
        [StringLength(50)]
        public string FirmNo { get; set; }

        /// <summary>
        /// ��ȡ�������ø��˼��
        /// </summary>
        /// <remark></remark>
        [DisplayName("���˼��")]
        [StringLength(200)]
        public string Intro { get; set; }

        /// <summary>
        /// ��ȡ��������ע��ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("ע��ʱ��")]
        [StringLength(50)]
        [Required]
        public DateTime RegAt { get; set; }

        /// <summary>
        /// ��ȡ���������Ƽ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ƽ���")]
        [StringLength(50)]
        public string Referral { get; set; }

        /// <summary>
        /// ��ȡ��������ͷ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("ͷ��")]
        [StringLength(50)]
        [Required]
        public string Pic { get; set; }

    }
}
