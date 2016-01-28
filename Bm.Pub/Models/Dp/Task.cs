using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ��������
    /// </summary>
    [DisplayName("��������")]
    public sealed class Task : IId, IStamp
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
        public string CustomerNo { get; set; }

        /// <summary>
        /// ��ȡ�������þ����˱��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����˱��")]
        [StringLength(49)]
        [Required]
        public string BrokerNo { get; set; }
        /// <summary>
        /// ��ȡ����������Ŀ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ŀ���")]
        [StringLength(49)]
        [Required]
        public string ProjectNo { get; set; }
        /// <summary>
        /// ��ȡ�������õ�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [StringLength(20)]
        [Required]
        public string CityNo { get; set; }

        /// <summary>
        /// ��ȡ�������ÿͻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͻ�����")]
        [StringLength(49)]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ�������ÿͻ��Ա�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͻ��Ա�")]
        [StringLength(5)]
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
        [StringLength(49)]
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
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// ��ȡ�������û���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(49)]
        [Required]
        public string No { get; set; }
        /// <summary>
        /// ��ȡ�������û����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����")]
        [StringLength(49)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// ��ȡ�������÷�������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������")]
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// ��ȡ�������û״̬
        /// </summary>
        /// <remark></remark>
        [DisplayName("�״̬")]
        [StringLength(49)]
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// ��ȡ����������Ӧ��ֹ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ӧ��ֹ��")]
        public DateTime Deadline { get; set; }

        /// <summary>
        /// ��ȡ�������û��ע
        /// </summary>
        /// <remark></remark>
        [DisplayName("���ע")]
        [StringLength(49)]
        public string Remark { get; set; }

        /// <summary>
        /// ��ȡ�������ûͼƬ
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ͼƬ")]
        [StringLength(49)]
        public string Pic { get; set; }
        /// <summary>
        /// ��ȡ�������û���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(49)]
        public string Result { get; set; }
        /// <summary>
        /// ��ȡ����������ҵ���ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ҵ���ʱ��")]
        [StringLength(49)]
        public string ConsultantNo { get; set; }
        /// <summary>
        /// ��ȡ����������ҵ������Ŀ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ҵ������Ŀ")]
        [StringLength(49)]
        public string ConsultantName { get; set; }
        /// <summary>
        /// ��ȡ����������ҵ���ʱ�ע
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ҵ���ʱ�ע")]
        [StringLength(49)]
        public string ConsultantRemark { get; set; }

        /// <summary>
        /// ��ȡ����������������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("��������������")]
        public DateTime ExpiredAt { get; set; }
    }
}
