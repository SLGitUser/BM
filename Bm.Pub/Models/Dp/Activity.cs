using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// �
    /// </summary>
    [DisplayName("�")]
    public sealed class Activity : IId, IStamp
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
        /// ��ȡ����������Ϣ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ϣ���")]
        [StringLength(49)]
        [Required]
        public string MessageNo { get; set; }

        /// <summary>
        /// ��ȡ����������Ŀ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ŀ���")]
        [StringLength(49)]
        [Required]
        public string ProjectNo { get; set; }
        /// <summary>
        /// ��ȡ����������Ϣ����
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ϣ����")]
        [StringLength(49)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// ʱ��
        /// </summary>
        [DisplayName("ʱ��")]
        [Required]
        public DateTime At { get; set; }

        /// <summary>
        /// ��ȡ�������ñ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(49)]
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(500)]
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// ��ȡ�������ñ���ͼƬ
        /// </summary>
        /// <remark></remark>
        [DisplayName("����ͼƬ")]
        [StringLength(50)]
        public string Pic { get; set; }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        [DisplayName("��ʼ����")]
        [Required]
        public DateTime BeginAt { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        [DisplayName("��������")]
        [Required]
        public DateTime ExpiredAt { get; set; }
    }
}
