using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bm.Models.Common;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ��Ϣ
    /// </summary>
    [DisplayName("��Ϣ")]
    public sealed class Message : IId, IStamp
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
        /// ��ȡ���������û����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�û����")]
        [StringLength(49)]
        [Required]
        public string UserNo { get; set; }

        /// <summary>
        /// ��ȡ����������Ϣ����
        /// </summary>
        /// <remark></remark>
        [DisplayName("��Ϣ����")]
        [StringLength(49)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// ��ȡ��������ʱ��
        /// </summary>
        /// <remark></remark>
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
        /// ��ȡ���������Ƿ��Ķ�
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ƿ��Ķ�")]
        [Required]
        public bool IsRead { get; set; }

        /// <summary>
        /// ��ȡ���������Ķ�ʱ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ķ�ʱ��")]
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// ��ȡ���������Ƿ�ɾ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�Ƿ�ɾ��")]
        [Required]
        public bool IsDel { get; set; }
    }
}
