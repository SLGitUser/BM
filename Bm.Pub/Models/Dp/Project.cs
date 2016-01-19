using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.senlang.Sdip.Data;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ������Ŀ
    /// </summary>
    [DisplayName("������Ŀ")]
    public sealed class Project: IId, IStamp
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
        /// ��ȡ�������ÿ����̱��
        /// </summary>
        /// <remark></remark>
        [DisplayName("�����̱��")]
        [StringLength(20)]
        [Required]
        public string OwnerNo { get; set; }
        
        /// <summary>
        /// ��ȡ�������ñ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("���")]
        [StringLength(20)]
        [Required]
        public string No { get; set; }
        
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ�������ð��
        /// </summary>
        /// <remark></remark>
        [DisplayName("���")]
        [StringLength(50)]
        public string BizGrp { get; set; }

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        public string BldType { get; set; }

        /// <summary>
        /// ��ȡ������������״̬
        /// </summary>
        /// <remark></remark>
        [DisplayName("����״̬")]
        [StringLength(20)]
        [Required]
        public string SaleStatus { get; set; }

        /// <summary>
        /// ��ȡ�������þ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        public int AvgPrice { get; set; }


        /// <summary>
        /// ��ȡ����������ѯ�绰
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ѯ�绰")]
        [StringLength(50)]
        public string SalesTel { get; set; }

        /// <summary>
        /// ��ȡ�������õ�ַ
        /// </summary>
        /// <remark></remark>
        [DisplayName("��ַ")]
        [StringLength(50)]
        public string Address { get; set; }

        /// <summary>
        /// ��ȡ�������þ���
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// ��ȡ��������γ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("γ��")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// ��ȡ��������λ��ͼƬ
        /// </summary>
        /// <remark></remark>
        [DisplayName("λ��ͼƬ")]
        [StringLength(50)]
        public string AddrPic { get; set; }

        /// <summary>
        /// ��ȡ���������ƿ���֤����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ƿ���֤����")]
        public string AuthType { get; set; }
    }
}
